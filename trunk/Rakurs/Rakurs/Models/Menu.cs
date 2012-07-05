using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rakurs.Models
{
    public class Menu:List<MenuItem>
    {

    }

    public class MenuItem
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public bool IsMainPage { get; set; }
        public bool Selected { get; set; }
    }
}