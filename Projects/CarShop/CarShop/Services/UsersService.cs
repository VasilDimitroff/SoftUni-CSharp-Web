using CarShop.Data;
using CarShop.Data.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CarShop.Services
{
    public class UsersService: IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string username, string email, string password, string userType)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Id = Guid.NewGuid().ToString(),
                Password = ComputeHash(password),
                IsMechanic = userType.ToLower() == "mechanic" ? true : false,
            };

            db.Users.Add(user);
            db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == ComputeHash(password));

            return user?.Id;
        }

        public bool IsUserMechanic(string Userid)
        {
            var user = db.Users.Find(Userid);

            return user.IsMechanic;
        }

        public bool IsUsernameAvailable(string username)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return true;
            }

            return false;
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
