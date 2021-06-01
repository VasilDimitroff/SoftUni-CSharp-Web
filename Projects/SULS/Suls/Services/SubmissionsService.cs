using Suls.Data;
using SulsApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Services
{
    public class SubmissionsService : ISubmissionsService
    {
        private readonly Random rand;
        private readonly ApplicationDbContext db;

        public SubmissionsService(Random rand, ApplicationDbContext db)
        {  
            this.rand = rand;
            this.db = db;
        }

        public string Create(string problemId, string code, string userId)
        {
            var problem = db.Problems.Find(problemId);

            var submission = new Submission()
            {
                Id = Guid.NewGuid().ToString(),
                AchievedResult = rand.Next(0, problem.Points),
                Code = code,
                CreatedOn = DateTime.UtcNow,
                ProblemId = problem.Id,
                UserId = userId,
            };

            db.Submissions.Add(submission);
            db.SaveChanges();

            return submission.Id;
        }

        public string Delete(string id)
        {
            var submission = db.Submissions.Find(id);

            db.Submissions.Remove(submission);

            db.SaveChanges();

            return submission.Id;
        }
    }
}
