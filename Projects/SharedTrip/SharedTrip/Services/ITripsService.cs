using SharedTrip.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        public string Add(string startPoint, string endPoint, string departureTime, string imagePath, int seats, string description);
        public IEnumerable<AllTripsViewModel> GetAll();
        public TripViewModel Details(string tripId);
        public void AddUserToTrip(string userId, string tripId);
        public bool IsUserInTrip(string userId, string tripId);
    }
}
