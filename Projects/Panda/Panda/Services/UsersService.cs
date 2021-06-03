using Panda.Data;
using Panda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Panda.Services
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
            if (!IsUsernameAvailable(username))
            {
                return;
            }

            if (!IsEmailAvailable(email))
            {
                return;
            }

            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Email = email,
                Password = ComputeHash(password)
            };

            db.Users.Add(user);
            db.SaveChanges();
        }

        public bool IsEmailAvailable(string email)
        {
            var targetEmail = db.Users.FirstOrDefault(x => x.Email == email);

            if (targetEmail == null)
            {
                return true;
            }

            return false;
        }

        public string GetUserId(string username, string password)
        {
            var hashPassword = ComputeHash(password);
            var user = this.db.Users
                .FirstOrDefault(u => u.Username == username && u.Password == hashPassword);

            if (user == null)
            {
                return null;
            }

            return user.Id;
        }

        public bool IsUsernameAvailable(string username)
        {
            var targetUsername = db.Users.FirstOrDefault(u => u.Username == username);

            if (targetUsername == null)
            {
                return true;
            }

            return false;
        }

        public string GetUsername(string id)
        {
            var user = db.Users.Find(id);

            return user?.Username;
        }

        public IEnumerable<UsernameViewModel> GetAllUsers()
        {
            var users = db.Users.Select(x => new UsernameViewModel
            {
                Username = x.Username
            }) 
                .ToList();

            return users;
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
