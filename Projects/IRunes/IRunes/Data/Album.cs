using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace IRunes.Data
{
    public class Album
    {
        public Album()
        {
            Tracks = new HashSet<Track>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Cover { get; set; }

        public decimal Price => this.Tracks.Sum(track => track.Price) * 0.87m;

        public virtual ICollection<Track> Tracks { get; set; }
    }
}
