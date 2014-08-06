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
        public decimal OldPrice { get; set; }
        public decimal Price { get; set; }
        public string ImageSource { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }


    }
}