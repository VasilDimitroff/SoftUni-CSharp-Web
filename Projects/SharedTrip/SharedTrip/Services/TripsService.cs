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

        public string Add(TripInputModel input)
        {
            DateTime depTime = DateTime
                .ParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

            var trip = new Trip
            {
                Id = Guid.NewGuid().ToString(),
                DepartureTime = depTime,
                Description = input.Description,
                EndPoint = input.EndPoint,
                ImagePath = input.ImagePath,
                StartPoint = input.StartPoint,
                Seats = input.Seats
            };

            this.db.Trips.Add(trip);
            this.db.SaveChanges();

            return trip.Id;
        }

        public IEnumerable<TripViewModel> GetAll()
        {
            var trips = db.Trips.Select(trip => new TripViewModel
            {
                Id = trip.Id,
                UsedSeats = trip.TripUsers.Count,
                Seats = trip.Seats,
                DepartureTime = trip.DepartureTime.ToString(),
                EndPoint = trip.EndPoint,
                StartPoint = trip.StartPoint
            })
                .ToList();

            return trips;
        }

        public TripViewModel GetDetails(string id)
        {
            TripViewModel trip = db.Trips
                .Where(trip => trip.Id == id)
                .Select(trip => new TripViewModel
                {
                    Id = trip.Id,
                    UsedSeats = trip.TripUsers.Count,
                    Seats = trip.Seats,
                    DepartureTime = trip.DepartureTime.ToString(),
                    EndPoint = trip.EndPoint,
                    Description = trip.Description,
                    StartPoint = trip.StartPoint,
                    ImagePath = trip.ImagePath
                })
                 .ToList()
                 .FirstOrDefault();

            return trip;
        }

        public void AddUserToTrip(string tripId, string userId)
        {
            var trip = db.Trips.FirstOrDefault(x => x.Id == tripId);
            var user = db.Users.FirstOrDefault(x => x.Id == userId);

            UserTrip userTrip = new UserTrip
            {
                TripId = trip.Id,
                Trip = trip,
                UserId = user.Id,
                User = user
            };

            db.UsersTrips.Add(userTrip);
            db.SaveChanges();
        }

        public bool IsUserInTrip(string tripId, string userId)
        {
            var trip = db.Trips.FirstOrDefault(x => x.Id == tripId);
            var user = db.Users.FirstOrDefault(x => x.Id == userId);

            if (db.UsersTrips.Any(x => x.UserId == userId && x.TripId == tripId))
            {
                return true;
            }

            return false;
        }
    }
}
