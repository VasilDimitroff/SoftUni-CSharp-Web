using SharedTrip.Data;
using SharedTrip.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(TripInputModel inputTrip)
        {
            Trip trip = new Trip
            {
                Id = Guid.NewGuid().ToString(),
                DepartureTime = DateTime.ParseExact(inputTrip.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Description = inputTrip.Description,
                EndPoint = inputTrip.EndPoint,
                ImagePath = inputTrip.ImagePath,
                StartPoint = inputTrip.StartingPoint,
                Seats = inputTrip.Seats,
            };

            db.Trips.Add(trip);

            db.SaveChanges();

            return;
        }

        public IEnumerable<TripViewModel> All()
        {
            var trips = db.Trips.Select(trip => new TripViewModel
            {
                Id = trip.Id.ToString(),
                DepartureTime = trip.DepartureTime.ToString(),
                Description = trip.Description,
                EndPoint = trip.EndPoint,
                StartingPoint = trip.StartPoint,
                ImagePath = trip.ImagePath,
                Seats = trip.Seats,
                UsedSeats = trip.UserTrips.Count
            })
                .ToList();

            return trips;
        }
    }
}
