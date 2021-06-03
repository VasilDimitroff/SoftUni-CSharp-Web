using Panda.Data;
using Panda.Data.Enumerations;
using Panda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panda.Services
{
    public class PackagesService : IPackagesService
    {
        private readonly ApplicationDbContext db;
        private readonly IReceiptsService receiptsService;
        private readonly IUsersService usersService;

        public PackagesService(ApplicationDbContext db, IReceiptsService receiptsService)
        {
            this.db = db;
            this.receiptsService = receiptsService;
        }

        public string Create(string description, double weight, string shippingAddress, string username)
        {
            var user = db.Users.FirstOrDefault(x => x.Username == username);

            var package = new Package
            {
                Id = Guid.NewGuid().ToString(),
                Description = description,
                EstimatedDeliveryDate = null,
                Recipient = user,
                ShippingAddress = shippingAddress,
                Weight = weight,
                Status = Enum.Parse<Status>("Pending"),
            };

            db.Packages.Add(package);
            db.SaveChanges();

            receiptsService.Create(user.Id, package.Id);

            return package.Id;
        }
        public IEnumerable<PackageViewModel> GetPending(string userId)
        {
            var packages = db.Packages
                .Where(x => x.Status == Status.Pending && x.RecipientId == userId)
                .Select(x => new PackageViewModel 
                { 
                  Description = x.Description,
                  Id = x.Id,
                  RecipientName = x.Recipient.Username,
                  ShippingAddress = x.ShippingAddress,
                  Weight =x.Weight
                })
                .ToList();

            return packages;
        }

        public IEnumerable<DeliveredPackageViewModel> GetDelivered(string userId)
        {
            var packages = db.Packages
                .Where(x => x.Status == Status.Delivered && x.RecipientId == userId)
                .Select(x => new DeliveredPackageViewModel
                {
                    Description = x.Description,
                    Id = x.Id,
                    RecipientName = x.Recipient.Username,
                    ShippingAddress = x.ShippingAddress,
                    Weight = x.Weight,
                    Status = x.Status.ToString(),
                })
                .ToList();

            return packages;
        }
        public string Deliver(string packageId)
        {
            var package = db.Packages.Find(packageId);
            package.Status = Status.Delivered;

            db.SaveChanges();

            return package.Id;
        }
    }
}
