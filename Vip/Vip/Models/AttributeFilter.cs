using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vip.Models
{
    public class AttributeFilter:List<FilterItem>
    {
        public string CurrentCategoryTitle { get; set; }
    }

    public class FilterItem
    {
        public string Title { get; set; }
        public List<FilterAttribute> Attributes { get; set; }
        public string Selector { get; set; }
    }

    public class FilterAttribute
    {
        public string Title { get; set; }
        public int Id { get; set; }
    }


}