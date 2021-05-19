using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.ViewModels
{
    public class TripViewModel
    {
        public string Id { get; set; }
        public string StartingPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartureTime { get; set; }
        public string DepartureTimeAsString => this.DepartureTime.ToString();
        public string ImagePath { get; set; }
        public int Seats { get; set; }
        public int AvailableSeats => this.Seats - this.UsedSeats;
        public int UsedSeats { get; set; } 
        public string Description { get; set; }
    }
}
