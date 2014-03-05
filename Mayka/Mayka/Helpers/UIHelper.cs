using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SiteExtensions;

namespace Mayka.Helpers
{
    public class ContentHeaderModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class UIMenuItem
    {

    }

    public class MenuItem
    {
        public string Title { get; set; }
        public int ContentId { get; set; }
        public string ContentName { get; set; }
        public bool Selected { get; set; }
        public bool Current { get; set; }
        public int SortOrder { get; set; }
        public ContentType ContentType { get; set; }
    }

    public enum ContentType
    {
        HomePage = 0,
        Content = 1,
        Gallery = 2,
        Products = 3
    }

    


    public class UIHelper
    {
        public static string[] ContentTypeNames =
        {
            "Домашняя страница", 
            "Страница контента",
            "Галерея",
            "Каталог товаров"
        };

        public static List<SelectListItem> GetContentTypes(byte? selectedItem)
        {
            var result = new List<SelectListItem>();
            var array = Enum.GetValues(typeof(ContentType)).Cast<ContentType>().ToArray();
            foreach (var contentType in array)
            {
                result.Add(new SelectListItem { Text = ContentTypeNames[(byte)contentType], Value = ((byte)contentType).ToString(), Selected = selectedItem == (byte)contentType });
            }
            return result;
        }

        public static List<SelectListItem> GetContentTypes()
        {
            return GetContentTypes(null);
        }
    }
}