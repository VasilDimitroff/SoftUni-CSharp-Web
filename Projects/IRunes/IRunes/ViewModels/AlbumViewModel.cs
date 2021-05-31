using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.ViewModels
{
   public class AlbumViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Cover { get; set; }

        public string Price { get; set; }

        public IEnumerable<TrackNameViewModel> Tracks { get; set; }
    }
}
