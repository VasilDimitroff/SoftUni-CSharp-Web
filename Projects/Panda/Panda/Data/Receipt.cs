using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panda.Data
{
    public class Receipt
    {
        [Required]
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime? IssuedOn { get; set; }

        [Required]
        [ForeignKey("User")]
        public string RecipientId { get; set; }

        public User Recipient { get; set; }

        [Required]
        public string PackageId { get; set; }

        public Package Package { get; set; }
    }
}