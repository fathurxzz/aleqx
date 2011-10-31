using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;

namespace HavilaTravel.Models
{

    public class Menu : List<MenuItem>
    {
        public int MenuLevel { get; set; }

        private static string _contentName;

        public static List<Menu> GetMenuList(string contentName, ContentStorage context)
        {
            _contentName = contentName;
            var result = new List<Menu>();
            string selectedItemName = null;
            var content = context.Content.Include("Parent").Include("Children").Where(c => c.Name == contentName).FirstOrDefault();
            if (content != null)
            {
                if (content.Children.Count > 0)
                {
                    result.Add(GetMenuFromContext(content.Children.ToList(), null));
                }

                while (content.Parent != null)
                {
                    selectedItemName = content.Name;
                    var parentId = content.Parent.Id;
                    content = context.Content.Include("Parent").Include("Children").Where(c => c.Id == parentId).First();
                    result.Add(GetMenuFromContext(content.Children.ToList(), selectedItemName));
                }
                selectedItemName = content.Name;
            }

            result.Add(GetTopLevelMenu(context, selectedItemName));

            return result;
        }

        public static Menu GetMenuFromContext(List<Content> contents, string selectedItemName)
        {
            var menu = new Menu();
            menu.AddRange(
                contents.Select(c => new MenuItem
                {
                    Id = (int)c.Id,
                    Name = c.Name,
                    Selected = selectedItemName == c.Name,
                    Title = c.MenuTitle,
                    SortOrder = c.SortOrder,
                    Current = c.Name == _contentName
                }));
            menu.MenuLevel = (int)contents.First().ContentLevel;
            return menu;
        }

        public static Menu GetTopLevelMenu(ContentStorage context, string selectedItemName)
        {
            var contents = context.Content.Where(m => m.ContentType == 1 && m.ContentLevel == 1).Select(m => m).ToList();
            var menu = GetMenuFromContext(contents, selectedItemName);
            return menu;
        }
    }

    public class MenuItem
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public bool Selected { get; set; }
        public int SortOrder { get; set; }
        public bool Current { get; set; }
    }
}