﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace EM2013.Models
{
    public class SiteViewModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }
        public string PageTitle { get; set; }

        public string Description { get; set; }

        public Content Content { get; set; }
        public List<SecretImage> SecretImages { get; set; }

        public SiteViewModel(SiteContext context, string contentName)
        {
            PageTitle = "Студия Евгения Миллера";
            var categories = context.Category;
            Menu = InitializeMainMenu(categories);

            if (!string.IsNullOrEmpty(contentName))
            {
                Content = context.Content.First(c => c.Name == contentName);
            }
            else if (contentName == "")
            {
                Content = context.Content.First(c => c.HomePage);
                IsHomePage = true;
            }
            
            if(Content!=null)
            {
                PageTitle += " » " + Content.Title;
                SeoDescription = Content.SeoDescription;
                SeoKeywords = Content.SeoKeywords;
                Title = Content.Title;
            }

            if (contentName=="secretlink")
            {
                SecretImages = context.SecretImage.ToList();
            }

        }

        private static Menu InitializeMainMenu(IEnumerable<Category> categories)
        {
            var menu = new Menu();
            var menuItems = categories.Select(c => new MenuItem
            {
                ContentId = c.Id,
                ContentName = c.Name,
                Title = c.Title,
                SortOrder = c.SortOrder
            });
            menu.AddRange(menuItems);
            return menu;
        }
    }
}