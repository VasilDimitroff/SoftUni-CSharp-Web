using SharedTrip.ViewModels;
using System.Collections.Generic;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void Add(string startPoint, string endPoint);

        IEnumerable<TripViewModel> All();
    }
}
