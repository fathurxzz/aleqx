using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kiki.DataAccess.Entities;

namespace Kiki.WebSite.Models
{
    public class SiteMenuModel
    {
        public IEnumerable<Content> Contents { get; set; }
        public string Lang { get; set; }
        public string CurrentMenuItemName { get; set; }
    }
}