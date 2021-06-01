using Suls.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Services
{
    public interface IProblemsService
    {
        public IEnumerable<ProblemIndexViewModel> GetAll();
        public ProblemDetailsViewModel Details(string id);
        public string Create(string name, int points);

    }
}
