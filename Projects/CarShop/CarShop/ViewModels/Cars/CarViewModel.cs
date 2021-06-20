using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.ViewModels.Cars
{
    public class CarViewModel
    {
        public string Id { get; set; }

        public string Model { get; set; }

        public string Image { get; set; }

        public string PlateNumber { get; set; }

        public int RemainingIssues { get; set; }

        public int Year { get; set; }

        public int FixedIssues { get; set; }
    }
}
