using Suls.Services;
using Suls.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionsService submissionsService;

        private readonly IProblemsService problemsService;

        public SubmissionsController(IProblemsService problemsService, ISubmissionsService submissionsService)
        {
            this.problemsService = problemsService;
            this.submissionsService = submissionsService;
        }

       

        public HttpResponse Create(string id)
        {
            var problem = problemsService.GetProblemById(id);
            var viewModel = new CreateSubmissionViewModel()
            {
                Name = problem.Name,
                ProblemId = problem.Id
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string problemId, string code)
        {
            submissionsService.Create(problemId, code, GetUserId());

            return this.Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
            submissionsService.Delete(id);

            return this.Redirect("/");
        }
    }
}
