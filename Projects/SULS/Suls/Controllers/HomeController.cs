using Suls.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Controllers
{
    public class HomeController: Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (IsUserSignedIn())
            {
                var loggedViewModel = new LoggedIndexViewModel();
                return this.View(loggedViewModel, "IndexLoggedIn");
            }
            var viewModel = new IndexGuestViewModel();

            return this.View(viewModel);
        }
    }
}
