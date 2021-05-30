using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface ICommitsService
    {
        public string Create(string repositoryId, string userId, string description);
        public IEnumerable<CommitViewModel> All(string userId);
        public void Delete(string userId, string commitId);
        public bool IsOwner(string userId, string commitId);
    }
}
