using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.ViewModels
{
    public class SubmissionViewModel
    {
        public string SubmissionId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string CreatedOn { get; set; }
        public int AchievedResult { get; set; }
        public int MaxPoints { get; set; }
    }
}
