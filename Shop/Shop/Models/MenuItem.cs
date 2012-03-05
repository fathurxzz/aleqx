using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public int SortOrder { get; set; }
    }
}