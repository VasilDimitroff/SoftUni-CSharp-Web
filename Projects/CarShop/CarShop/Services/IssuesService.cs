using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Issues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarShop.Services
{
    public class IssuesService : IIssuesService
    {
        private readonly ApplicationDbContext db;

        public IssuesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(string carId, string description)
        {
            var issue = new Issue
            {
                CarId = carId,
                Description = description,
                Id = Guid.NewGuid().ToString(),
                IsFixed = false
            };

            db.Issues.Add(issue);
            db.SaveChanges();
        }

        public void Delete(string issueId, string carId)
        {
            var issueForDelete = db.Issues.FirstOrDefault(x => x.Id == issueId && x.CarId == carId);

            if (issueForDelete == null)
            {
                return;
            }

            db.Issues.Remove(issueForDelete);
            db.SaveChanges();
        }

        public int Fix(string userId, string issueId, string carId)
        {
            var user = db.Users.Find(userId);

            if (!user.IsMechanic)
            {
                return -1;
            }

            var issue = db.Issues.FirstOrDefault(x => x.Id == issueId && x.CarId == carId);

            issue.IsFixed = true;

            db.SaveChanges();

            return 1;
        }

        public CarIssuesViewModel GetAll(string carId)
        {
            var targetCar = db.Cars.Find(carId);
            var carModel = new CarIssuesViewModel();
            carModel.Model = targetCar.Model;
            carModel.Year = targetCar.Year;
            carModel.CarId = carId;
            carModel.Issues = db.Issues
                .Where(i => i.CarId == carId)
                .Select(i => new IssueViewModel
                {
                    Id = i.Id,
                    Description = i.Description,
                    IsFixed = i.IsFixed,
                })
                .ToList();

            return carModel;
        }
    }
}
