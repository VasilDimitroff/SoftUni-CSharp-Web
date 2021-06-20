using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarShop.Services
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;

        public CarsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int Add(string userId, string model, int year, string imagePath, string plateNumber)
        {
            var user = db.Users.Find(userId);

            if (user.IsMechanic)
            {
                return -1;
            }

            var car = new Car
            {
                Id = Guid.NewGuid().ToString(),
                Model = model,
                OwnerId = userId,
                PictureUrl = imagePath,
                PlateNumber = plateNumber,
                Year = year,
            };

            db.Cars.Add(car);

            db.SaveChanges();

            return 1;
        }

        public IEnumerable<CarViewModel> AllByUser(string userId)
        {
            var cars = db.Cars
                .Where(c => c.OwnerId == userId)
                .Select(c => new CarViewModel
                {
                    Id = c.Id,
                    Image = c.PictureUrl,
                    Model = c.Model,
                    PlateNumber = c.PlateNumber,
                    Year = c.Year,
                    FixedIssues = c.Issues.Count(i => i.IsFixed == true),
                    RemainingIssues = c.Issues.Count(i => i.IsFixed == false)
                })
                .ToList();

            return cars;
        }

        public IEnumerable<CarViewModel> All()
        {
            var cars = db.Cars
                .Where(c => c.Issues.Any(i => i.IsFixed == false))
                .Select(c => new CarViewModel
                {
                    Id = c.Id,
                    Image = c.PictureUrl,
                    Model = c.Model,
                    PlateNumber = c.PlateNumber,
                    Year = c.Year,
                    FixedIssues = c.Issues.Count(i => i.IsFixed == true),
                    RemainingIssues = c.Issues.Count(i => i.IsFixed == false)
                })
                .ToList();

            return cars;
        }

        public bool IsUserMechanic(string userId)
        {
            var user = db.Users.Find(userId);

            return user.IsMechanic;
        }
    }
}
