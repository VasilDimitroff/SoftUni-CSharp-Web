using CarShop.ViewModels.Cars;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Services
{
    public interface ICarsService
    {
        public int Add(string userId, string model, int year, string imagePath, string plateNumber);

        public IEnumerable<CarViewModel> AllByUser(string userId);

        public IEnumerable<CarViewModel> All();

        public bool IsUserMechanic(string userId);
    }
}
