using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Kulumu.Models
{
    public partial class Category
    {
        public bool Selected { get; set; }
        public bool IsParent { get; set; }

        public static Category InitCategory(Category c, IDataRecord dr)
        {
            c.Id = dr.GetValue<int>("Id");
            return c;
        }

    }


}