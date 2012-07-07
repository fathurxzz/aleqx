using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rakurs.Models
{
    public class Menu:List<MenuItem>
    {

    }

    public class RakursSiteMenu : List<RakursMenuItem>
    {

    }

    public class MenuItem
    {
        public int ContentId { get; set; }
        public string ContentName { get; set; }
        public string Title { get; set; }
        public int SortOrder { get; set; }
        public bool IsMainPage { get; set; }
        public bool Selected { get; set; }
    }


    public class RakursMenuItem:MenuItem
    {
        public bool IsGalleryMenuItem { get; set; }

    }
}