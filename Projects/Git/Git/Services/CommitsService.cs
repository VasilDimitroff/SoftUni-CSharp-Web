using Git.Data;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CommitViewModel> All(string userId)
        {
            var commits = db.Commits
                 .Where(x => x.CreatorId == userId)
                 .Select(x => new CommitViewModel
                 {
                     Id = x.Id,
                     CreatedOn = x.CreatedOn.ToString(),
                     Description = x.Description,
                     Repository = x.Repository.Name
                 })
                 .ToList();

            return commits;
        }

        public string Create(string repositoryId, string userId, string description)
        {
            var commit = new Commit
            {
                CreatedOn = DateTime.UtcNow,
                CreatorId = userId,
                Description = description,
                RepositoryId = repositoryId,
                Id = Guid.NewGuid().ToString(),
            };

            db.Commits.Add(commit);
            db.SaveChanges();

            return commit.Id;
        }

        public void Delete(string commitId)
        {
            var commit =
                db.Commits.FirstOrDefault(x => x.Id == commitId);

                db.Commits.Remove(commit);
                db.SaveChanges();
        }

        public bool IsOwner(string userId, string commitId)
        {
            var commit =
                db.Commits.FirstOrDefault(x => x.CreatorId == userId && x.Id == commitId);

            if (commit == null)
            {
                return false;
            }

            return true;
        }
    }
}
