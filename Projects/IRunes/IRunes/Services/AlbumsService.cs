using IRunes.Data;
using IRunes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.Services
{
    public class AlbumsService : IAlbumsService
    {
        private readonly ApplicationDbContext db;

        public AlbumsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string Create(string name, string cover)
        {

            var album = new Album
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Cover = cover
            };

            db.Albums.Add(album);
            db.SaveChanges();

            return album.Id;
        }

        public AlbumViewModel Details(string albumId)
        {
            var album = db.Albums
                .Select(x => new AlbumViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Cover = x.Cover,
                    Price = (x.Tracks.Sum(t => t.Price) * 0.87m).ToString("f2"),
                    Tracks = x.Tracks.Select(t => new TrackNameViewModel
                    {
                        Name = t.Name,
                        Id = t.Id
                    })
                    .ToList()
                })
                .FirstOrDefault(x => x.Id == albumId);

            return album;
        }

        public IEnumerable<AlbumAllViewModel> GetAll()
        {
            var albums = db.Albums
                .Select(x => new AlbumAllViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                 .ToList();

            return albums;
        }
    }
}
