using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rakurs.Models
{
    public class PathItem
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
    }

    public partial class Product
    {
        public List<PathItem> Path { get; set; }
    }
}