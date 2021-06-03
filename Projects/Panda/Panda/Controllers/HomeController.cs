using Panda.Services;
using Panda.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (IsUserSignedIn())
            {
                var user = new HelloUserViewModel();
                user.Username = usersService.GetUsername(GetUserId());
                return this.View(user, "IndexLoggedIn");
            }

            return this.View();
        }
    }
}
