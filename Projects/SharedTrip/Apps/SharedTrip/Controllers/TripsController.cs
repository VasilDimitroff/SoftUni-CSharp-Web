using SharedTrip.Services;
using SharedTrip.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripService;

        public TripsController(ITripsService tripService)
        {
            this.tripService = tripService;
        }

        public HttpResponse Add()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(string startPoint, string endPoint, string departureTime, string imagePath, int seats, string description)
        {
            if (string.IsNullOrWhiteSpace(startPoint))
            {
                return this.Error("Start Point cannot be empty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(endPoint))
            {
                return this.Error("End Point cannot be empty or whitespace");
            }

            if (!DateTime.TryParseExact(departureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return this.Error("Invalid datetime format");
            }

            TripInputModel tripInput = new TripInputModel() 
            {
                DepartureTime = departureTime,
                Description = description,
                EndPoint = endPoint,
                ImagePath = imagePath,
                Seats = seats,
                StartingPoint = startPoint
            };

            tripService.Add(tripInput);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse All()
        {
            var trips = this.tripService.All();
            return this.View(trips);
        }

        public HttpResponse Details(string tripId)
        {
            var trip = this.tripService.GetTripById(tripId);

            return this.View(trip);
        }
    }
}
