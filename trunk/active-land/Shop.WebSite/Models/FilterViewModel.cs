using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.DataAccess.Entities;

namespace Shop.WebSite.Models
{
    public class FilterViewModel
    {
        public List<FilterItem> FilterItems { get; set; }
        public string Title { get; set; }
    }

    public class FilterItem
    {
        public string Id { get; set; }
        public bool Selected { get; set; }
        public string Title { get; set; }
        public int AvaibleProductsCount { get; set; }
        public string FilterAttributeString { get; set; }
    }
}