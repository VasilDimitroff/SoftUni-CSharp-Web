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

        public string Add(string startPoint, string endPoint, string departureTime, string imagePath, int seats, string description)
        {
            var trip = new Trip
            {
                Id = Guid.NewGuid().ToString(),
                DepartureTime = DateTime.Parse(departureTime),
                Description = description,
                EndPoint = endPoint,
                ImagePath = imagePath,
                Seats = seats,
                StartPoint = startPoint,
            };

            db.Add(trip);
            db.SaveChanges();

            return trip.Id;
        }

        public void AddUserToTrip(string userId, string tripId)
        {
            var user = db.Users.Find(userId);
            var trip = db.Trips.Find(tripId);

            var userTrip = new UserTrip
            {
                TripId = trip.Id,
                UserId = user.Id
            };

            db.UsersTrips.Add(userTrip);
            db.SaveChanges();
        }

        public TripViewModel Details(string tripId)
        {
            var trip = db.Trips
                .Select(x => new TripViewModel
                {
                    AvailableSeats = x.Seats - x.TripUsers.Count(),
                    DepartureTime = x.DepartureTime.ToString(),
                    Description = x.Description,
                    EndPoint = x.EndPoint,
                    StartPoint = x.StartPoint,
                    Id = x.Id,
                    ImagePath = x.ImagePath
                })
                .FirstOrDefault(x => x.Id == tripId);

            return trip;
        }

        public IEnumerable<AllTripsViewModel> GetAll()
        {
            var trips = db.Trips
                .Select(x => new AllTripsViewModel
                {
                    AvailableSeats = x.Seats - x.TripUsers.Count(),
                    DepartureTime = x.DepartureTime.ToString(),
                    EndPoint = x.EndPoint,
                    StartPoint = x.StartPoint,
                    Id = x.Id,
                })
                .ToList();

            return trips;
        }

        public bool IsUserInTrip(string userId, string tripId)
        {
           return db.UsersTrips.Any(x => x.UserId == userId && x.TripId == tripId);
        }
    }
}
