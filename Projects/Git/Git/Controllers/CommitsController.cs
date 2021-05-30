using Git.Services;
using Git.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly IRepositoriesService repositoriesService;
        private readonly ICommitsService commitsService;

        public CommitsController(IRepositoriesService repositoriesService, ICommitsService commitsService)
        {
            this.repositoriesService = repositoriesService;
            this.commitsService = commitsService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var repository = repositoriesService.GetRepositoryById(id);

            var viewModel = new CreateCommitViewModel();
            viewModel.Id = repository.Id;
            viewModel.Name = repository.Name;

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string id, string description)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(description) || description.Length < 5)
            {
                return this.Error("Description must be minimum 5 characters long");
            }

            commitsService.Create(id, this.GetUserId(), description);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var allCommits = commitsService.All(this.GetUserId());

            return this.View(allCommits);
        }


        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!commitsService.IsOwner(GetUserId(), id))
            {
                return this.Error("Only the creator can delete commits");
            }

            commitsService.Delete(id);

            return this.Redirect("/Repositories/All");
        }
    }
}
