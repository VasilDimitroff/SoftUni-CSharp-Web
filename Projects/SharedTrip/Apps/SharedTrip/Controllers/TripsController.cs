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
        public HttpResponse Add()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripInputModel input)
        {
            if (string.IsNullOrWhiteSpace(input.StartingPoint))
            {
                return this.Error("Start Point cannot be empty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(input.EndPoint))
            {
                return this.Error("End Point cannot be empty or whitespace");
            } 

            return this.View();
        }

        public HttpResponse All()
        {
            return this.View();
        }
    }
}
