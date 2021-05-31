using IRunes.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Services
{
    public interface ITracksService
    {
        public string Create(string albumId, string name, string link, decimal price);
        public TrackViewModel Details(string albumId, string trackId);
    }
}
