using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        public IEnumerable<RepositoryViewModel> GetAllPublic();
        public string Create(string userId, string name, string repositoryType);
    }
}
