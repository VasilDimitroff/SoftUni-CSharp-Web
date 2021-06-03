using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.ViewModels
{
    public class ReceiptViewModel
    {
        public string Id { get; set; }
        public decimal Fee { get; set; }
        public string IssuedOn { get; set; }
        public string Recipient { get; set; }
    }
}
