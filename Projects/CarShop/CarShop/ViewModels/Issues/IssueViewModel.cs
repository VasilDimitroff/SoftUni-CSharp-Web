using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.ViewModels.Issues
{
    public class IssueViewModel
    {
        public string Id { get; set; }

        public bool IsFixed { get; set; }

        public string Description { get; set; }
    }
}
