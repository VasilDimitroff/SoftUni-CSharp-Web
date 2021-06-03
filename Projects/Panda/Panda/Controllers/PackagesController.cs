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
            var users = usersService.GetAllUsers();
            return this.View(users);
        }

        [HttpPost]
        public HttpResponse Create(PackageInputModel input)
        {
            packagesService.Create(input.Description, input.Weight, input.ShippingAddress, input.RecipientName);
            return this.Redirect("/");
        }

        public HttpResponse Pending()
        {
            var packages = packagesService.GetPending(GetUserId());
            return this.View(packages);
        }

        public HttpResponse Delivered()
        {
            var packages = packagesService.GetDelivered(GetUserId());
            return this.View(packages);
        }

        public HttpResponse Deliver(string id)
        {
            packagesService.Deliver(id);
            return this.Redirect("/");
        }
    }
}
