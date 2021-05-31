using IRunes.Services;
using IRunes.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;

        public TracksController(ITracksService tracksService)
        {
            this.tracksService = tracksService;
        }

        public HttpResponse Create(string albumId)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            AlbumIdViewModel viewModel = new AlbumIdViewModel
            {
                AlbumId = albumId
           };
            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(TrackInputModel input)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (input.Name == null || input.Name.Length < 4 || input.Name.Length > 20)
            {
                return this.Error("Track name must be between 4 and 20 characters long");
            }

            if (string.IsNullOrWhiteSpace(input.Link) || !input.Link.StartsWith("http"))
            {
                return this.Error("Please enter a valid link");
            }

            if (input.Price == null)
            {
                return this.Error("Please enter a price");
            }

            tracksService.Create(input.AlbumId, input.Name, input.Link, input.Price);

            return this.Redirect($"/Albums/Details?id={input.AlbumId}");
            
        }

        public HttpResponse Details(string albumId, string trackId)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var track = tracksService.Details(albumId, trackId);
            return this.View(track);
        }
    }
}
