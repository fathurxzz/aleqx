﻿using System.Collections.Generic;
using System.Linq;
using Mayka.Helpers;
using Mayka.Models.Entities;
using SiteExtensions;

namespace Mayka.Models
{
    public class SiteModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsHomePage { get; set; }
        public Content Content { get; set; }
        public Product Product { get; set; }
        public List<Helpers.MenuItem> Menu  { get; set; }
        protected IQueryable<Content> Contents { get; set; }

        public SiteModel(SiteContext context, string contentId, int? productId = null)
        {
            Contents = context.Content;
            Title = "Майкаджексон";

            if (contentId == null)
            {
                Product = context.Product.First(p => p.Id == productId);
                Content = Product.Content;
            }
            else
            {
                Content = Contents.FirstOrDefault(c => c.Name == contentId) ??
                          context.Content.First(c => c.ContentType == (int) ContentType.HomePage);
            }

            Menu = new List<Helpers.MenuItem>();

            foreach (var c in Contents
                //.Where(c=>c.ContentType!=(int)ContentType.HomePage)
                )
            {
                Menu.Add(new Helpers.MenuItem
                {
                    ContentId = c.Id,
                    ContentName = c.Name,
                    Current = c.Name == contentId,
                    Selected = c.Name==Content.Name,
                    SortOrder = c.SortOrder,
                    Title = c.MenuTitle,
                    ContentType = (ContentType)c.ContentType
                });
            }

            SeoDescription = Content.SeoDescription;
            SeoKeywords = Content.SeoKeywords;
        }

        
    }
}