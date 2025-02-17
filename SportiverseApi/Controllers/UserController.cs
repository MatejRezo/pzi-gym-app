using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using SportiverseApi.Models.Auth;
using SportiverseApi.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SportiverseApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;
        
        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        [HttpGet]
        [Authorize(Roles = "Owner,Trainer")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return Ok(await _dataContext.Users.Include(u => u.UserRole).ToListAsync());
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<List<User>>> DeleteUser(int userId)
        {
            var user = await _dataContext.Users.FindAsync(userId);

            if (user == null) return BadRequest("User not found");
            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Users.Include(u => u.UserRole).ToListAsync());
        }

        [HttpPatch]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<List<User>>> UpdateUser(User updatedUser)
        {
            var user = await _dataContext.Users.FindAsync(updatedUser.Id);
            var role = await _dataContext.Roles.FindAsync(updatedUser.UserRole.Id);

            if (user == null) return BadRequest("User not found");
            if (role == null) return BadRequest("Role not found");

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Username = updatedUser.Username;
            user.UserRole = role;
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Users.Include(u => u.UserRole).ToListAsync());
        }

        [HttpPatch("update-role")]
        [Authorize(Roles="Owner")]
        public async Task<ActionResult<List<User>>> UpdateUserRole(int userId, int roleId)
        {
            var user = await _dataContext.Users.FindAsync(userId);
            var role = await _dataContext.Roles.FindAsync(roleId);

            if (user == null) return BadRequest("User not found");
            if (role == null) return BadRequest("Role not found");

            user.UserRole = role;
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Users.Include(u => u.UserRole).ToListAsync());
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<User>> getCurrentUser()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString()
                .Replace("bearer ", "")
                .Replace("Bearer ", "");
            var username = ExtractUsernameFromToken(_bearer_token);
            var user = await _dataContext.Users
                    .Include(u => u.UserRole)
                    .FirstOrDefaultAsync(_user => _user.Username == username);

            if (user == null) return BadRequest("User not found");

            return Ok(user);
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
