using Panda.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Controllers
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
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = usersService.GetUserId(username, password);

            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }

            SignIn(userId);

            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(string username, string email, string password, string confirmPassword)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (!usersService.IsEmailAvailable(email))
            {
                return this.Error("Email already taken!");
            }

            if (!usersService.IsUsernameAvailable(username))
            {
                return this.Error("Username already taken!");
            }

            if (string.IsNullOrWhiteSpace(username) || username.Length < 4 || username.Length > 10)
            {
                return this.Error("Username must be between 4 and 10 characters long!");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return this.Error("Please enter a valid email!");
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6 || password.Length > 20)
            {
                return this.Error("Password must be between 6 and 20 characters long!");
            }

            if (password != confirmPassword)
            {
                return this.Error("Passwords must match!");
            }

            usersService.Create(username, email, password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (IsUserSignedIn())
            {
                this.SignOut();
            }

            return this.Redirect("/");
        }
    }
}
