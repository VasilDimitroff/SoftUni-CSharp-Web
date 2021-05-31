using IRunes.Services;
using IRunes.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Controllers
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
            if (this.IsUserSignedIn())
            {
                var username = usersService.GetUsername(this.GetUserId());
                var viewModel = new SignedInIndexViewModel()
                {
                    Username = username
                };

                return this.View(viewModel, "Home");
            }

            return this.View();
        }
    }
}
