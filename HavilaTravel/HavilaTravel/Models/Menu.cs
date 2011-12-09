﻿using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;

namespace HavilaTravel.Models
{

    public class Menu : List<MenuItem>
    {
        public int MenuLevel { get; set; }
        public int ContentType { get; set; }

        private static string _contentName;

        public static List<Menu> GetMenuList(string contentName, ContentStorage context, out Content possibleCurrentContent)
        {
            _contentName = contentName;
            var result = new List<Menu>();
            string selectedItemName = null;
            Content content = null;
            if (!string.IsNullOrEmpty(contentName))
            {
                content = context.Content.Include("Parent").Include("Children").Include("Accordions")
                    .Where(c => c.Name == contentName).FirstOrDefault();
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
            }


            var menuMainLevels = GetMenuMainLevels(context);
            result.Add(GetTopLevelMenu(menuMainLevels, selectedItemName));
            result.Add(GetHeaderLeftMenu(menuMainLevels, selectedItemName));

            possibleCurrentContent = content;
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
                    Selected = selectedItemName == c.Name,
                    Title = c.MenuTitle,
                    SortOrder = c.SortOrder,
                    Current = c.Name == _contentName
                }));
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