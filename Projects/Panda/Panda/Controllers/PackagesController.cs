using Panda.Services;
using Panda.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IPackagesService packagesService;
        private readonly IUsersService usersService;

        public PackagesController(IPackagesService packagesService, IUsersService usersService)
        {
            this.packagesService = packagesService;
            this.usersService = usersService;
        }

        public HttpResponse Create()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var users = usersService.GetAllUsers();
            return this.View(users);
        }

        [HttpPost]
        public HttpResponse Create(PackageInputModel input)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(input.Description) || input.Description.Length < 5 || input.Description.Length > 20)
            {
                return this.Redirect("/Packages/Create");
            }

            if (string.IsNullOrWhiteSpace(input.RecipientName))
            {
                return this.Redirect("/Packages/Create");
            }

            packagesService.Create(input.Description, input.Weight, input.ShippingAddress, input.RecipientName);
            return this.Redirect("/Packages/Pending");
        }

        public HttpResponse Pending()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var packages = packagesService.GetPending(GetUserId());
            return this.View(packages);
        }

        public HttpResponse Delivered()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var packages = packagesService.GetDelivered(GetUserId());
            return this.View(packages);
        }

        public HttpResponse Deliver(string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            packagesService.Deliver(id);
            return this.Redirect("/Packages/Delivered");
        }
    }
}
