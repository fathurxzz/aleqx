using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public partial class Category
    {
        public int Level { get; set; }
        public bool Selected { get; set; }
    }
}