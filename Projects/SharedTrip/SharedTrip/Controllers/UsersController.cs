using SharedTrip.Services;
using SharedTrip.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
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
            if (!IsUserSignedIn())
            {
                this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel inputModel)
        {
            if (IsUserSignedIn())
            {
                this.Redirect("/");
            }

            var user = this.usersService.GetUserId(inputModel.Username, inputModel.Password);
            this.SignIn(user);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            if (IsUserSignedIn())
            {
                this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (IsUserSignedIn())
            {
                this.Redirect("/");
            }

            if (input.Username.Length < 5 || input.Username.Length > 20 || string.IsNullOrWhiteSpace(input.Username))
            {
                return this.Error("Username must be between 5 and 20 characters and can't be empty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(input.Email))
            {
                return this.Error("Invalid email!");
            }

            if (input.Password.Length < 6 || input.Password.Length > 20 || string.IsNullOrWhiteSpace(input.Password))
            {
                return this.Error("Password must be between 6 and 20 symbols");
            }


            if (!usersService.IsEmailAvailable(input.Email))
            {
                return this.Error("Email already taken");
            }

            if (!usersService.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username already taken");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords do not match");
            }

            usersService.Create(input.Username, input.Email, input.Password);

            return this.Redirect("/Users/Login");
        }
    }
}
