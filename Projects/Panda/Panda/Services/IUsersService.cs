using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Services
{
    public interface IUsersService
    {
        public string GetUserId(string username, string password);
        public bool IsUsernameAvailable(string username);
        public bool IsEmailAvailable(string email);
        public void Create(string username, string email, string password);
    }
}
