using Suls.Data;
using Suls.ViewModels;
using SulsApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suls.Services
{
    public class ProblemsService : IProblemsService
    {
        private readonly ApplicationDbContext db;

        public ProblemsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string Create(string name, int points)
        {
            var problem = new Problem
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Points = points
            };

            db.Problems.Add(problem);
            db.SaveChanges();

            return problem.Id;
        }

        public ProblemDetailsViewModel Details(string id)
        {
            var submission = db.Problems
                .Where(x => x.Id == id)
                .Select(x => new ProblemDetailsViewModel
                {
                    Name = x.Name,
                    Problems = x.Submissions.Select(s => new SubmissionViewModel
                    {
                        SubmissionId = s.Id,
                        Name = s.Problem.Name,
                        AchievedResult = s.AchievedResult,
                        CreatedOn = s.CreatedOn.ToString(),
                        MaxPoints = s.Problem.Points,
                        Username = s.User.Username
                    })
                    .ToList()
                })
                .FirstOrDefault();
                

            return submission;
        }

        public IEnumerable<ProblemIndexViewModel> GetAll()
        {
            var problems = db.Problems
                .Select(x => new ProblemIndexViewModel
            {
                Id = x.Id,
                Count = x.Submissions.Count(),
                Name = x.Name
            })
                .ToList();

            return problems;
        }

        public ProblemIndexViewModel GetProblemById(string id)
        {
            var problem = db.Problems
                .Select(x => new ProblemIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.Submissions.Count()
                })
                .FirstOrDefault(x => x.Id == id);

            return problem;
        }
    }
}
