using IRunes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IRunes.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string username, string email, string password)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                Password = ComputeHash(password),
                Username = username
            };

            db.Users.Add(user);
            db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var user = db.Users.FirstOrDefault(x => (x.Username == username && x.Password == ComputeHash(password)) || (x.Email == username && x.Password == ComputeHash(password)));

            return user?.Id;
        }

        public string GetUsername(string userId)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);

            return user?.Username;
        }

        public bool IsEmailAvailable(string email)
        {
            return !db.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
             return !db.Users.Any(x => x.Username == username);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
