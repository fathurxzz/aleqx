using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Kulumu.Models
{
    public class CategoryProductPresentation
    {
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryName { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDiscountText { get; set; }
        public int ProductId { get; set; }
        public string ProductImageSource { get; set; }

        public static CategoryProductPresentation Init(CategoryProductPresentation c, IDataRecord dr)
        {
            c.CategoryId = dr.GetValue<int>("CategoryId");
            c.CategoryTitle = dr.GetValue<string>("CategoryTitle");
            c.CategoryName = dr.GetValue<string>("CategoryName");
            c.ProductTitle = dr.GetValue<string>("ProductTitle");
            c.ProductDiscountText = dr.GetValue<string>("DiscountText");
            c.ProductId = dr.GetValue<int>("ProductId");
            c.ProductImageSource = dr.GetValue<string>("ImageSource");
            return c;
        }

        

    }
}