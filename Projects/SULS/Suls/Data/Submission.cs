using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Suls.Data
{
    public class Submission
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Code { get; set; }

        public int AchievedResult { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string ProblemId { get; set; }

        public Problem Problem { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
