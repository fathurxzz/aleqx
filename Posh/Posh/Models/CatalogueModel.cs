using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Posh.Helpers;

namespace Posh.Models
{
    public class CatalogueModel : SiteViewModel
    {
        public Album Album { get; set; }
        public List<Album> Albums { get; set; }

        public CatalogueModel(ModelContainer dataContext, string contentId, string albumId, bool loadContent = true)
            : base(dataContext, contentId, albumId, loadContent)
        {
            Albums = _context.Album.Include("Products").ToList();

            if (!string.IsNullOrEmpty(albumId))
            {
                Album = _context.Album.Include("Products").First(a => a.Name == albumId);
                Title += " - " + Album.Title;
            }
        }

        //public void ApplyFilter(List<Category> checkedCategories, List<Element> checkedElements)
        //{
        //    WebSession.Categories.AddRange(checkedCategories);
        //    WebSession.Elements.AddRange(checkedElements);

        //    Categories = Categories.Where(checkedCategories.Contains).ToList();
        //    Elements = Elements.Where(checkedElements.Contains).ToList();
        //}
    }
}