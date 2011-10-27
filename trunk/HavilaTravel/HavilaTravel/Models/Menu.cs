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


        public static List<Menu> GetMenuList(string contentName, ContentStorage context)
        {
            var result = new List<Menu>();
            string selectedItemName = null;
            var content = context.Content.Include("Parent").Include("Children").Where(c => c.Name == contentName).FirstOrDefault();
            if (content != null)
            {
                if (content.Children.Count > 0)
                {
                    result.Add(GetMenuFromContext(content.Children, null));
                }

                while (content.Parent != null)
                {
                    selectedItemName = content.Name;
                    var parentId = content.Parent.Id;
                    content = context.Content.Include("Parent").Include("Children").Where(c => c.Id == parentId).First();
                    result.Add(GetMenuFromContext(content.Children, selectedItemName));
                }
                selectedItemName = content.Name;
            }


            result.Add(GetTopLevelMenu(context, selectedItemName));

            return result;
        }

        public static Menu GetMenuFromContext(EntityCollection<Content> contents, string selectedItemName)
        {
            var menu = new Menu();
            menu.AddRange(
                contents.Select(c => new MenuItem
                {
                    Id = (int)c.Id,
                    Name = c.Name,
                    Selected = selectedItemName == c.Name,
                    Title = c.Title,
                    SortOrder = c.SortOrder
                }));
            menu.MenuLevel = (int)contents.First().ContentLevel;
            return menu;
        }

        public static Menu GetTopLevelMenu(ContentStorage context, string selectedItemName)
        {
            var menu = new Menu();
            var contents = context.Content.Where(m => m.ContentType == 1 && m.ContentLevel == 1).Select(m => m).ToList();
            menu.AddRange(contents.Select(c => new MenuItem
            {
                Id = (int)c.Id,
                Name = c.Name,
                Selected = selectedItemName == c.Name,
                Title = c.Title,
                SortOrder = c.SortOrder
            }));
            menu.MenuLevel = (int)contents.First().ContentLevel;
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
    }
}