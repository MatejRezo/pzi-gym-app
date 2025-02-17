using System.Security.Cryptography;
using System.Text;
using SportiverseApi.Models.User;
using SportiverseApi.Models.Auth; // Assuming Role is defined here
using Microsoft.EntityFrameworkCore;

namespace SportiverseApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            // Seed Roles if none exist.
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Owner" },
                    new Role { Name = "Trainer" },
                    new Role { Name = "User" }
                };

                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            // Look for any existing users.
            if (context.Users.Any())
            {
                return; // DB has been seeded
            }

            var ownerRole = context.Roles.FirstOrDefault(r => r.Name == "Owner");
            var trainerRole = context.Roles.FirstOrDefault(r => r.Name == "Trainer");

            // Ensure the roles exist.
            if (ownerRole == null || trainerRole == null)
            {
                throw new Exception("Required roles were not found.");
            }

            // Seed the superAdmin user
            CreateUser(context, "superAdmin", "superAdmin", ownerRole);

            // Seed the admin user
            CreateUser(context, "admin", "admin", trainerRole);

            context.SaveChanges();
        }

        private static void CreateUser(DataContext context, string username, string password, Role role)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = username, // You can customize these as needed
                LastName = "User",
                UserRole = role,
                Token = string.Empty // Token will be set when the user logs in
            };

            context.Users.Add(user);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
