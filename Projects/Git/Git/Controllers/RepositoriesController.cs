using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            var repositories = repositoriesService.GetAllPublic();

            return this.View(repositories);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }   

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, string repositoryType)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || name.Length > 10)
            {
                return this.Error("Name must be between 3 and 10 characters long");
            }

            if (repositoryType.ToLower() != "public" && repositoryType.ToLower() != "private")
            {
                return this.Error("Please enter a valid repository type (Public or Private)");
            }
    
            var userId = this.GetUserId();

            repositoriesService.Create(userId, name, repositoryType);

            return this.Redirect("/Repositories/All");
        }
    }
}
