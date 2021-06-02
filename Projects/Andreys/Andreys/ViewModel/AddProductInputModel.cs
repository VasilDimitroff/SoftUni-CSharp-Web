using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.ViewModel
{
    public class AddProductInputModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }
        public decimal Price { get; set; }
    }
}
