using Andreys.Data;
using Andreys.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andreys.Services
{
    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }

        public int Add(string name, string description, string imageUrl, string category, string gender, decimal price)
        {
            var product = new Product()
            {
                Category = Enum.Parse<Category>(category),
                Gender = Enum.Parse<Gender>(gender),
                Description = description,
                ImageUrl = imageUrl,
                Name = name,
                Price = price
            };

            db.Products.Add(product);
            db.SaveChanges();

            return product.Id;
        }

        public void Delete(int productId)
        {
            var product = db.Products.Find(productId);
            db.Remove(product);
            db.SaveChanges();
        }

        public ProductViewModel Details(int productId)
        {
            var product = db.Products
                .Select(x => new ProductViewModel
                {
                    Category = x.Category.ToString(),
                    Description = x.Description,
                    Gender = x.Gender.ToString(),
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Name = x.Name,
                    Price = x.Price
                })
                .FirstOrDefault(x => x.Id == productId);

            return product;
        }

        public IEnumerable<ProductViewModel> All()
        {
            var products = db.Products
                .Select(x => new ProductViewModel
                {
                    Category = x.Category.ToString(),
                    Description = x.Description,
                    Gender = x.Gender.ToString(),
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Name = x.Name,
                    Price = x.Price
                })
                .ToList();

            return products;
        }
    }
}
