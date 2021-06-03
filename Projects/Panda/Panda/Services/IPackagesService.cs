using Panda.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Services
{
    public interface IPackagesService
    {
        public string Create(string description, double weight, string shippingAddress, string username);
        public IEnumerable<PackageViewModel> GetPending(string userId);
        public IEnumerable<DeliveredPackageViewModel> GetDelivered(string userId);
        public string Deliver(string packageId);
    }
}
