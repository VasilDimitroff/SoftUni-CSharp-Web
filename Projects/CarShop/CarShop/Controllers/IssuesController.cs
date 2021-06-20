using CarShop.Services;
using CarShop.ViewModels.Issues;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssuesService issuesService;

        public IssuesController(IIssuesService issuesService)
        {
            this.issuesService = issuesService;
        }

        public HttpResponse CarIssues(string carId)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var model = new CarIssuesViewModel();
            model = issuesService.GetAll(carId);

            return View(model);
        }
        public HttpResponse Add(string carId)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            return View(carId);
        }

        [HttpPost]
        public HttpResponse Add(string carId, string description)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(description) || description.Length < 5)
            {
                return Error("Description must be more than 5 characters long");
            }

            issuesService.Add(carId, description);
            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        public HttpResponse Delete(string issueId, string carId)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            issuesService.Delete(issueId, carId);
            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        public HttpResponse Fix(string issueId, string carId)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            int result = issuesService.Fix(GetUserId(), issueId, carId);

            if (result == -1)
            {
                return Error("Only mechanics can fix issues");
            }

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }
    }
}
