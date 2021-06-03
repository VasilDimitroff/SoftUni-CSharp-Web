using Panda.Data.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panda.Data
{
    public class Package
    {
        public Package()
        {
            Receipts = new HashSet<Receipt>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public Status Status { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        [Required]
        [ForeignKey("User")]
        public string RecipientId { get; set; }

        public User Recipient { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}