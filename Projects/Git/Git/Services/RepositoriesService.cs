using Git.Data;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string Create(string userId, string name, string repositoryType)
        {
            bool isPublic = true;

            if (repositoryType.ToLower() == "private")
            {
                isPublic = false;
            }

            var repository = new Repository
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
                IsPublic = isPublic,
                Name = name,
                OwnerId = userId,
            };

            db.Repositories.Add(repository);
            db.SaveChanges();

            return repository.Id;
        }

        public IEnumerable<RepositoryViewModel> GetAllPublic()
        {
            var repositories = db.Repositories.Where(x => x.IsPublic == true)
                .Select(x => new RepositoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CommitsCount = x.Commits.Count(),
                    CreatedOn = x.CreatedOn.ToString(),
                    Owner = x.Owner.Username
                })
                .ToList();

            return repositories;
        }

        public RepositoryViewModel GetRepositoryById(string repositoryId)
        {
            var repository = db.Repositories
             .Select(x => new RepositoryViewModel
             {
                 Id = x.Id,
                 Name = x.Name,
                 CommitsCount = x.Commits.Count(),
                 CreatedOn = x.CreatedOn.ToString(),
                 Owner = x.Owner.Username
             })
              .FirstOrDefault();


            return repository;
        }
    }
}
