using Andreys.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Services
{
    public interface IProductsService
    {
        public int Add(string name, string description, string imageUrl, string category, string gender, decimal price);
        public void Delete(int productId);
        public ProductViewModel Details(int productId);
        public IEnumerable<ProductViewModel> All();
    }
}
