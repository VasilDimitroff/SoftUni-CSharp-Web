using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;
using Andreys.Data;
using Microsoft.EntityFrameworkCore;
using Andreys.Services;

namespace Andreys.App
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProductsService, ProductsService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new AndreysDbContext().Database.Migrate();
        }
    }
}
