using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Git.Data
{
    public class Repository
    {
        public Repository()
        {
            Commits = new HashSet<Commit>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Commit> Commits { get; set; }
    }
}
