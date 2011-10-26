using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HavilaTravel.Models
{

    public class Menu : List<MenuItem>
    {
        public int MenuLevel { get; set; }
    }

    public class MenuItem
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public bool Selected { get; set; }
    }
}