using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.ViewModels
{
    public class TrackViewModel
    {
        public string Id { get; set; }
        public string AlbumId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string IFrameSource { get; set; }
    }
}
