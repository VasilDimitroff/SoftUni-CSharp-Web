using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Git.Data
{
    public class Commit
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public string RepositoryId { get; set; }

        public Repository Repository { get; set; }
    }
}
