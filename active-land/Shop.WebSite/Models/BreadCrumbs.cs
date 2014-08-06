using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebSite.Models
{
    public class BreadCrumb
    {
        public string Title;
        public string Name;
    }

    public class BreadCrumbs : List<BreadCrumb>
    {
        
    }
}