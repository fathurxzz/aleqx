using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kulumu.Models
{
    public class SubCategoriesMenu
    {
        public IEnumerable<Category> Categories { get; set; }
        public int ParentCategoryId { get; set; }
        public bool SelectedAsLink { get; set; }
    }
}