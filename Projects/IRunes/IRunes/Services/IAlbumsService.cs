using IRunes.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Services
{
    public interface IAlbumsService
    {
        public IEnumerable<AlbumAllViewModel> GetAll();
        public string Create(string name, string cover);
        public AlbumViewModel Details(string albumId);
    }
}
