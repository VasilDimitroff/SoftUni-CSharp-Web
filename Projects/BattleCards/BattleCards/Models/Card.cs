using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BattleCards.Models
{
    public class Card
    {
        public Card()
        {
            CardUsers = new HashSet<UserCard>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Keyword { get; set; }

        public int Attack { get; set; }

        public int Health { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<UserCard> CardUsers { get; set; }
    }
}
