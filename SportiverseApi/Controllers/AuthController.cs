using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SportiverseApi.Models.Auth;
using SportiverseApi.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SportiverseApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(Register request)
        {
            var userList = await _dataContext.Users.ToListAsync();
            var user = userList.FirstOrDefault(user =>
                VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)
                || user.Username == request.Username
            );
            var role = await _dataContext.Roles.FirstOrDefaultAsync(_role => _role.Name == "User");
            if (user != null) return BadRequest("User already exists.");
            if (role == null)
            {
                _dataContext.Roles.Add(new Role { Name = "User" });
                await _dataContext.SaveChangesAsync();
                role = await _dataContext.Roles.FirstOrDefaultAsync(_role => _role.Name == "User");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User _user = new User();
            _user.Username = request.Username;
            _user.FirstName = request.FirstName;
            _user.LastName = request.LastName;
            _user.PasswordHash = passwordHash;
            _user.PasswordSalt = passwordSalt;
            _user.UserRole = role;

            _dataContext.Add(_user);
            await _dataContext.SaveChangesAsync();
            return Ok(_user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthToken>> Login(Login request)
        {
            var user = await _dataContext.Users.Include(u => u.UserRole).FirstOrDefaultAsync(_user => _user.Username == request.Username);
            if (user == null) return BadRequest("User not found.");

            var userList = await _dataContext.Users.ToListAsync();
            user = userList.FirstOrDefault(user => VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt));
            if (user == null) return BadRequest("Wrong password.");

            AuthToken token = new AuthToken();
            token.Token = CreateToken(user);
            token.RefreshToken = GenerateRefreshToken();
            SetRefreshToken(token);
            //store token in db?
            user.Token = token.Token;
            await _dataContext.SaveChangesAsync();

            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthToken>> RefreshToken(AuthToken token)
        {
            string? refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken)) return BadRequest("Invalid token.");

            var _username = ExtractUsernameFromToken(token.Token);
            var user = await _dataContext.Users.FirstOrDefaultAsync(_user => _user.Username == _username);
            if (user == null) return BadRequest("User not found.");
            if (string.IsNullOrEmpty(user.Token)) return BadRequest("User not logged in");
            if (user.Token != token.Token) return BadRequest("Bad token");

            AuthToken newToken = new AuthToken();
            newToken.Token = CreateToken(user);
            newToken.RefreshToken = GenerateRefreshToken();
            SetRefreshToken(newToken);
            //store token in db?
            user.Token = newToken.Token;
            await _dataContext.SaveChangesAsync();

            return Ok(newToken);
        }

        [HttpPost("roles")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<List<Role>>> AddRole(string roleName)
        {
            _dataContext.Roles.Add(new Role { Name = roleName });
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Roles.ToListAsync());
        }

        [HttpGet("roles")]
        [Authorize(Roles = "Owner,Trainer")]
        public async Task<ActionResult<List<Role>>> GetRoles()
        {
            var roles = await _dataContext.Roles.ToListAsync();
            return Ok(roles);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        private void SetRefreshToken(AuthToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserRole.Name)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string ExtractUsernameFromToken(string token)
        {
            string secret = "G3F$0sgAX2766fth";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            return claims.Identity.Name;
        }
    }
}
