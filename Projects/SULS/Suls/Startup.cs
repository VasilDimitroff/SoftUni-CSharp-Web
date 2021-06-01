﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Suls.Services;
using SulsApp.Data;
using SUS.HTTP;
using SUS.MvcFramework;
namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
