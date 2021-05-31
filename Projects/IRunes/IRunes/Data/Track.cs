using System.ComponentModel.DataAnnotations;

namespace IRunes.Data
{
    public class Track
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Cover { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string AlbumId { get; set; }

        public Album Album { get; set; }
    }
}