using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebSite.Models
{
    public class ProductPreview
    {
        public ProductPreviewLabel Label { get; set; }
        public string Title { get; set; }
        public string OldPrice { get; set; }
        public string Price { get; set; }
        public string ImageSource { get; set; }
    }
}