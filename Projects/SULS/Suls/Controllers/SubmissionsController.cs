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

        public HttpResponse Create()
        {
            var viewModel = new CreateSubmissionViewModel();
            viewModel.ProblemId = "5";
            viewModel.Name = "test";
            return this.View(viewModel);
        }
    }
}
