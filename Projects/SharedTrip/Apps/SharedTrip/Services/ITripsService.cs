using SharedTrip.ViewModels;
using System.Collections.Generic;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        public void Add(TripInputModel inputTrip);

        public IEnumerable<TripViewModel> All();

        public TripViewModel GetTripById(string id);
    }
}
