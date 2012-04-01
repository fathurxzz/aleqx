using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Posh.Helpers;

namespace Posh.Models
{
    public class SiteViewModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Content Content { get; set; }
        public List<MenuItem> MainMenu { get; set; }
        public bool IsHomePage { get; set; }
        public List<Category> Categories { get; set; }
        public List<Element> Elements { get; set; }

        public Album Album { get; set; }
        public List<Album> Albums { get; set; }
        public List<News> News { get; set; }

 

        private readonly ModelContainer _context;

        public SiteViewModel(ModelContainer dataContext, string contentId, string albumId, bool loadContent = true, bool loadCatalogue = false, bool loadNews=false)
        {
            Title = "Posh. Обустройство вашего заведения";
            _context = dataContext;

            Categories = _context.Category.ToList();
            Elements = _context.Element.ToList();
            News = _context.News.ToList();

            MainMenu = new List<MenuItem>();

            var contents = _context.Content.ToList();
            foreach (var c in contents)
            {
                MainMenu.Add(new MenuItem { Id = c.Id, Name = c.Name, Title = c.Title, Current = c.Name == contentId, Selected = string.IsNullOrEmpty(albumId) && c.Name == contentId, SortOrder = c.SortOrder, Static = c.Static, MainPage = c.MainPage });
            }

            if(loadCatalogue)
            {
                Albums = _context.Album.ToList();
            }

            if(!string.IsNullOrEmpty(albumId))
            {
                Album = _context.Album.Include("Products").First(a => a.Name == albumId);
            }


            if (loadContent)
            {
                Content content = null;
                if (!string.IsNullOrEmpty(contentId))
                {
                    content = _context.Content.FirstOrDefault(c => c.Name == contentId);
                    if (content == null)
                    {
                        throw new HttpNotFoundException();
                    }
                }
                else
                {
                    content = _context.Content.FirstOrDefault(c => c.MainPage);
                    IsHomePage = true;
                }

                Content = content;
                if (content != null)
                {
                    Title += " - " + content.Title;
                    SeoDescription = content.SeoDescription;
                    SeoKeywords = content.SeoKeywords;
                }
            }

        }
    }
}