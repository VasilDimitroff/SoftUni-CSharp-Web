using SharedTrip.Services;
using SharedTrip.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }

            var trips = tripsService.GetAll();
            return this.View(trips);
        }

        public HttpResponse Add()
        {
            if (!IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripInputModel input)
        {
            if (!IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(input.StartPoint))
            {
                return this.Redirect("/Trips/Add");
            }

            if (string.IsNullOrWhiteSpace(input.EndPoint))
            {
                return this.Redirect("/Trips/Add");
            }

            if (string.IsNullOrWhiteSpace(input.DepartureTime))
            {
                return this.Redirect("/Trips/Add");
            }

            if (input.Seats < 2 || input.Seats > 6)
            {
                return this.Redirect("/Trips/Add");
            }

            if (string.IsNullOrWhiteSpace(input.Description) || input.Description.Length > 80)
            {
                return this.Redirect("/Trips/Add");
            }

            tripsService.Add(input.StartPoint, input.EndPoint, input.DepartureTime, input.ImagePath, input.Seats, input.Description);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (!IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }

            var trip = tripsService.Details(tripId);
            return this.View(trip);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }

            if (tripsService.IsUserInTrip(GetUserId(), tripId))
            {
                return this.Redirect($"/Trips/Details?tripId={tripId}");
            }

            tripsService.AddUserToTrip(GetUserId(),tripId);
            return this.Redirect("/Trips/All");
        }
    }
}
