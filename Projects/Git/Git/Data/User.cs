using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Git.Data
{
    public class User
    {
        public User()
        {
            Repositories = new HashSet<Repository>();
            Commits = new HashSet<Commit>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Repository> Repositories { get; set; }
        public ICollection<Commit> Commits { get; set; }
    }
}
