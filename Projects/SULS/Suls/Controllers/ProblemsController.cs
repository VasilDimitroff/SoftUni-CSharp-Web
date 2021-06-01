using Suls.Services;
using Suls.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suls.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemsService problemsService;

        public ProblemsController(IProblemsService problemsService)
        {
            this.problemsService = problemsService;
        }

        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            LoggedIndexViewModel loggedViewModel = new LoggedIndexViewModel();
            loggedViewModel.Problems = problemsService.GetAll().ToList();

            return this.View(loggedViewModel);
        }

        public HttpResponse Create()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, int points)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            problemsService.Create(name, points);
            return this.Redirect("/Problems/All");
        }

        public HttpResponse Details(string id)
        {
            var submissions = problemsService.Details(id);
            return this.View(submissions);
        }
    }
}
