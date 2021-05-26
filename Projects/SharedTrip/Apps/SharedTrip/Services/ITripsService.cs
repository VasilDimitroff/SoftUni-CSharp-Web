using SharedTrip.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        public IEnumerable<TripViewModel> GetAll();
        public string Add(TripInputModel input);
        public TripViewModel GetDetails(string id);
        public void AddUserToTrip(string tripId, string userId);
        public bool IsUserInTrip(string tripId, string userId);
    }
}
