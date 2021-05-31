using IRunes.Services;
using IRunes.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            this.albumsService = albumsService;
        }

        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var albums = albumsService.GetAll();

            return this.View(albums);
        }

        public HttpResponse Create()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, string cover)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(name) || name.Length < 4 || name.Length > 20)
            {
                return this.Error("Album name must be between 4 and 20 characters long");
            }

            if (string.IsNullOrWhiteSpace(cover) || !cover.StartsWith("http"))
            {
                return this.Error("Please enter a valid image path");
            }

            albumsService.Create(name, cover);

            return this.Redirect("/Albums/All");
        }

        public HttpResponse Details(string id)
        {
            if (!IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }

            AlbumViewModel album = albumsService.Details(id);

            return this.View(album);
        }
    }
}
