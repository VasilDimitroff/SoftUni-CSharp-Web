using IRunes.Data;
using IRunes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.Services
{
    public class TracksService : ITracksService
    {
        private readonly ApplicationDbContext db;

        public TracksService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string Create(string albumId, string name, string link, decimal price)
        {
            var track = new Track
            {
                Id = Guid.NewGuid().ToString(),
                AlbumId = albumId,
                Cover = link,
                Price = price,
                Name = name,
            };

            db.Tracks.Add(track);
            db.SaveChanges();

            return track.Id;
        }

        public TrackViewModel Details(string albumId, string trackId)
        {
            var track = db.Tracks
                .Select(x => new TrackViewModel
                {
                    Id = x.Id,
                    IFrameSource = x.Cover,
                    Name = x.Name,
                    Price = x.Price,
                    AlbumId = x.AlbumId
                })
                .FirstOrDefault(x => x.AlbumId == albumId && x.Id == trackId);

            return track;
        }
    }
}
