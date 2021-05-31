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
            var trips = tripsService.GetAll();
            return this.View(trips);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            tripsService.Add(input);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var trip = tripsService.GetDetails(tripId);

            return this.View(trip);
        }

        
        public HttpResponse AddUserToTrip(string tripId)
        {
          
            string userId = this.GetUserId();

            if (!tripsService.IsUserInTrip(tripId, userId))
            {
                tripsService.AddUserToTrip(tripId, userId);

                return this.Redirect($"/Trips/All");
            }

            return this.Redirect($"/Trips/All");
        }
    }
}
