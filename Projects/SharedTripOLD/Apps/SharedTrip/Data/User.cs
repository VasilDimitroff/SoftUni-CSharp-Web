using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedTrip.Data
{
    public class User
    {
        [Required]
        public string Id { get; set; } = new Guid().ToString();

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<UserTrip> UserTrips { get; set; } = new HashSet<UserTrip>();

    }
}