using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Posh.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public bool Current { get; set; }
        public int SortOrder { get; set; }
        public bool Static { get; set; }
        public bool MainPage { get; set; }
    }
}