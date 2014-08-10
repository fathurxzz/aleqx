using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.DataAccess.Entities;

namespace Shop.WebSite.Models
{
    public class MainMenuModel
    {
        public IEnumerable<Content> Contents { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}