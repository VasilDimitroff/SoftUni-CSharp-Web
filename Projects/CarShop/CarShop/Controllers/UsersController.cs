using CarShop.Services;
using CarShop.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Controllers
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
                this.Redirect("/Cars/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (IsUserSignedIn())
            {
                this.Redirect("/Cars/All");
            }

            var userId = usersService.GetUserId(username, password);

            if (userId == null)
            {
                return this.Error("Username and password do not match");
            }

            SignIn(userId);

            return this.Redirect("/Cars/All");
        }

        public HttpResponse Register()
        {
            if (IsUserSignedIn())
            {
                this.Redirect("/Cars/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (IsUserSignedIn())
            {
                this.Redirect("/Cars/All/");
            }

            if (!usersService.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username already taken");
            }

            if (string.IsNullOrWhiteSpace(input.Username) || input.Username.Length < 4 || input.Username.Length > 20)
            {
                return this.Error("Username length must be between 4 and 20 characters long");
            }

            if (string.IsNullOrWhiteSpace(input.Email) || !input.Email.Contains("@"))
            {
                return this.Error("Please enter a valid email");
            }

            if (string.IsNullOrWhiteSpace(input.Password) || input.Password.Length < 5 || input.Password.Length > 20)
            {
                return this.Error("Password must be between 5 and 20 characters long");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords must match");
            }

            if (input.UserType.ToLower() != "mechanic" && input.UserType.ToLower() != "client")
            {
                return this.Error("User Type must be Mechanic or Client");
            }

            usersService.Create(input.Username, input.Email, input.Password, input.UserType);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            SignOut();

            return this.View();
        }
    }
}
