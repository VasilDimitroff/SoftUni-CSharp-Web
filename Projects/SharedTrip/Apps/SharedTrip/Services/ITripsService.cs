using SharedTrip.ViewModels;
using System.Collections.Generic;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void Add(TripInputModel inputTrip);

        public IEnumerable<TripViewModel> All();
    }
}
