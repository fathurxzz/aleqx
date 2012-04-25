using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using HavilaTravel.Helpers;

namespace HavilaTravel.Models
{

    public class Menu : List<MenuItem>
    {
        public int MenuLevel { get; set; }
        public int ContentType { get; set; }

        private static string _contentName;

        public static List<Menu> GetMenuList(string contentName, ContentStorage context, bool loadSibs, out Content possibleCurrentContent)
        {
            _contentName = contentName;
            var result = new List<Menu>();
            string selectedItemName = null;
            possibleCurrentContent = null;

            if (!string.IsNullOrEmpty(contentName))
            {
                Content content = context.Content.Include("Parent").Include("Children").Include("Accordions").FirstOrDefault(c => c.Name == contentName);
                if (content != null)
                {
                    possibleCurrentContent = content;
                    if (content.Children.Count > 0)
                    {
                        result.Add(GetMenuFromContext(content.Children.ToList(), null));
                    }

                    if (loadSibs)
                    {
                        while (content.Parent != null)
                        {
                            selectedItemName = content.Name;
                            var parentId = content.Parent.Id;
                            content = context.Content.Include("Parent").Include("Children").First(c => c.Id == parentId);
                            result.Add(GetMenuFromContext(content.Children.ToList(), selectedItemName));
                        }
                    }
                    selectedItemName = content.Name;
                }
            }

            var menuMainLevels = GetMenuMainLevels(context);

            Menu menu = GetTopLevelMenu(menuMainLevels, selectedItemName);
            if (menu != null)
                result.Add(menu);
            menu = GetHeaderLeftMenu(menuMainLevels, selectedItemName);
            if (menu != null)
                result.Add(menu);
            return result;
        }


        private static List<Content> GetMenuMainLevels(ContentStorage context)
        {
            return context.Content.Where(m => m.ContentType == 1 && m.ContentLevel == 1 || m.ContentType == 10).Select(m => m).ToList();
        }


        private static Menu GetMenuFromContext(List<Content> contents, string selectedItemName)
        {
            var menu = new Menu();
            menu.AddRange(
                contents.Select(c => new MenuItem
                {
                    Id = (int)c.Id,
                    Name = c.Name,
                    Selected = selectedItemName == c.Name || (c.Name.ToLower() == "countries" && WebSession.CurrentMenuHighlight == CurrentMenuHighlight.Country) || (c.Name.ToLower() == "spa" && WebSession.CurrentMenuHighlight == CurrentMenuHighlight.Spa),
                    Title = c.MenuTitle,
                    SortOrder = c.SortOrder,
                    Current = c.Name == _contentName
                }));
            if(!contents.Any())
            {
                return null;
            }
            menu.MenuLevel = (int)contents.First().ContentLevel;
            menu.ContentType = (int)contents.First().ContentType;
            return menu;
        }

        private static Menu GetTopLevelMenu(List<Content> contents, string selectedItemName)
        {
            contents = contents.Where(m => m.ContentType == 1 && m.ContentLevel == 1).Select(m => m).ToList();
            var menu = GetMenuFromContext(contents, selectedItemName);
            return menu;
        }

        private static Menu GetHeaderLeftMenu(List<Content> contents, string selectedItemName)
        {
            contents = contents.Where(m => m.ContentType == 10).Select(m => m).ToList();
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