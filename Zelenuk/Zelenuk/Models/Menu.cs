using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zelenuk.Models
{
    public class Menu : List<MenuItem>
    {
        public static Menu GetMenuFromContext(ContentStorage context, string currentContentName)
        {
            var contents = context.Content.ToList();
            
            var menu = new Menu();

            menu.AddRange(
                contents.Select(c => new MenuItem
                {
                    Id = c.Id,
                    Url = c.Name,
                    Selected = currentContentName==c.Name,
                    Title = c.Title,
                    SortOrder = c.SortOrder
                }));

            return menu;
        }
    }

    public class MenuItem 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool Selected { get; set; }
        public int SortOrder { get; set; }
    }
}