using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rakurs.Helpers;

namespace Rakurs.Models
{
    public class SiteViewModel
    {
        public Content Content { get; set; }
        public Menu MainMenu { get; set; }
        public bool IsHomePage { get; set; }
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        private readonly StructureContainer _context;

        public SiteViewModel(StructureContainer context, string contentName, bool loadContent = true)
        {
            Title = "Ракурс";
            _context = context;

            var contentList = _context.Content.ToList();
            FetchMainMenuItems(contentList);

            if (loadContent)
            {
                Content content;
                if (!string.IsNullOrEmpty(contentName))
                {
                    content = contentList.Where(c => c.Name == contentName).FirstOrDefault();
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

        private void FetchMainMenuItems(IEnumerable<Content> contentList)
        {
            var menuItems = contentList.Select(c => new MenuItem { Name = c.Name, Title = c.Title, SortOrder = c.SortOrder });
            foreach (var menuItem in menuItems)
            {
                MainMenu.Add(menuItem);
            }
        }

    }
}