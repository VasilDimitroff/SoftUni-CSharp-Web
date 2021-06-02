using Andreys.Services;
using Andreys.ViewModel;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var products = productsService.All();
            return this.View(products);
        }

        public HttpResponse Add()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddProductInputModel input)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(input.Name) || input.Name.Length < 4 || input.Name.Length > 20)
            {
                return this.Error("Name must be between 4 and 20 characters long");
            }

            if (input.Description.Length > 10)
            {
                return this.Error("Description must be max 10 characters long");
            }

            if (!input.ImageUrl.StartsWith("http"))
            {
                return this.Error("Please enter a valid image URL");
            }

            productsService.Add(input.Name, input.Description, input.ImageUrl, input.Category, input.Gender, input.Price);
            return this.Redirect("/");
        }

        public HttpResponse Details(int id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var product = productsService.Details(id);

            return this.View(product);
        }

        public HttpResponse Delete(int id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            productsService.Delete(id);

            return this.Redirect("/");
        }
    }
}
