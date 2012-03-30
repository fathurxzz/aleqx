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

        private readonly ModelContainer _context;

        public SiteViewModel(ModelContainer context, string contentId, bool loadContent = true)
        {
            Title = "Posh. Обустройство вашего заведения";
            _context = context;

            Categories = _context.Category.ToList();
            Elements = _context.Element.ToList();

            MainMenu = new List<MenuItem>();

            var contents = context.Content.ToList();
            foreach (var c in contents)
            {
                MainMenu.Add(new MenuItem { Id = c.Id, Name = c.Name, Title = c.Title, Selected = c.Name == contentId, SortOrder = c.SortOrder });
            }

            if (loadContent)
            {
                Content content = null;
                if (!string.IsNullOrEmpty(contentId))
                {
                    content = context.Content.FirstOrDefault(c => c.Name == contentId);
                    if (content == null)
                    {
                        throw new HttpNotFoundException();
                    }
                }
                else
                {
                    content = context.Content.FirstOrDefault(c => c.MainPage);
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