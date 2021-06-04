using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.ViewModels
{
    public class AllTripsViewModel
    {
        public string Id { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public string DepartureTime { get; set; }

        public int AvailableSeats { get; set; }
    }
}
