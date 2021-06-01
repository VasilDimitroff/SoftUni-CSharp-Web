using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Suls.Data
{
    public class Problem
    {
        public Problem()
        {
            Submissions = new HashSet<Submission>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Points { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
