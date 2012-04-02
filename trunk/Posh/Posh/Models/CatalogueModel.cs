using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Posh.Models
{
    public class CatalogueModel:SiteViewModel
    {
        public Album Album { get; set; }
        public List<Album> Albums { get; set; }

        public CatalogueModel(ModelContainer dataContext, string contentId, string albumId, bool loadContent=true) : base(dataContext, contentId, loadContent)
        {
            Albums = _context.Album.ToList();

            if (!string.IsNullOrEmpty(albumId))
            {
                Album = _context.Album.Include("Products").First(a => a.Name == albumId);
            }
        }

    }
}