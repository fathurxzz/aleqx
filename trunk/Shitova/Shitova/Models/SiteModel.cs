using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Shitova.Models
{
    public class SiteModel:ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }
        public IEnumerable<ContentItem> ContentItems { get; set; }
        public string PageTitle { get; set; }
        public bool HasContentItems { get; set; }

        public Content Content { get; set; }

        public SiteModel(SiteContainer context, string contentName, bool showContentItems = false)
        {
            Title = "Ольга Шитова";
            PageTitle = "Ольга Шитова";
            if (contentName == null)
            {
                Content = context.Content.First(c => c.MainPage);
            }
            else
            {
                Content = context.Content.FirstOrDefault(c => c.Name == contentName);
                if (Content == null)
                {
                    throw new HttpNotFoundException();
                }
            }

            if (!string.IsNullOrEmpty(Content.Title))
            {
                if (Content.Title != PageTitle)
                    PageTitle += " » " + Content.Title;
                Title = Content.Title;
            }

            SeoDescription = Content.SeoDescription;
            SeoKeywords = Content.SeoKeywords;

            if (Content.MainPage)
            {
                IsHomePage = true;
            }


            
            if (showContentItems)
            {
                HasContentItems = true;
                ContentItems = context.ContentItem.OrderBy(ci => ci.SortOrder).ToList();
            }

            var contents = context.Content.Where(c => !c.MainPage).ToList();

            Menu = new Menu();
            Menu.AddRange(contents.Select(content => new MenuItem {ContentId = content.Id, ContentName = content.Name, Current = content.Name == contentName, SortOrder = content.SortOrder, Title = content.Title}));
        }



    }
}