using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = usersService.GetUserId(username, password);

            if (userId == null)
            {
                return this.Error("Invalid username or password.");   
            }

            this.SignIn(userId);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(string username, string email, string password, string confirmPassword)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (!usersService.IsEmailAvailable(email))
            {
                return this.Error("Email already taken");
            }

            if (!usersService.IsUsernameAvailable(username))
            {
                return this.Error("Username already taken");
            }

            if (string.IsNullOrWhiteSpace(username) || username.Length < 5 || username.Length > 20)
            {
                return this.Error("Username must be between 5 and 20 characters long");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return this.Error("Please enter a valid email");
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6 || password.Length > 20)
            {
                return this.Error("Password must be between 5 and 20 characters long");
            }

            if (confirmPassword != password)
            {
                return this.Error("Passwords do not match.");
            }

            usersService.CreateUser(username, email, password);

            return this.Redirect("/Users/Login");
        }
    }
}
