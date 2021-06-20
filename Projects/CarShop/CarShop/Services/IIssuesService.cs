using CarShop.ViewModels.Issues;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Services
{
    public interface IIssuesService
    {
        public CarIssuesViewModel GetAll(string carId);

        public void Add(string carId, string description);

        public void Delete(string issueId, string carId);

        public int Fix(string userId, string issueId, string carId);
    }
}
