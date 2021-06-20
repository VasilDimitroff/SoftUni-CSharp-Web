using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.ViewModels.Cars
{
    public class AddCarInputModel
    {
        public string Model { get; set; }

        public string Image { get; set; }

        public string PlateNumber { get; set; }

        public int Year { get; set; }
    }
}
