using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Ego.Models
{
    public class OrderModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Menu Menu { get; set; }
        public bool IsHomePage { get; set; }

        public Order Order { get; set; }

        public OrderModel(SiteContainer context, int orderId)
        {
            Title = HttpUtility.HtmlDecode("Я &mdash; ЭГО");

            var contents = context.Content.ToList();
            Menu = new Menu();
            foreach (var content in contents)
            {
                Menu.Add(new MenuItem
                {
                    ContentId = content.Id,
                    ContentName = content.Name,
                    Selected = false,
                    SortOrder = content.SortOrder,
                    Title = content.Title
                });
            }

            Order = context.Order.First(o => o.Id == orderId);
        }
    }
}