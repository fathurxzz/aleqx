using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.DataAccess.Entities;

namespace Shop.WebSite.Helpers
{
    public class ProductHelper
    {
        IQueryable<Product> ApplyPaging(IQueryable<Product> products, int? page, int pageSize)
        {
            if (products == null)
                return null;
            int currentPage = page ?? 0;
            if (page < 0)
                return products;
            return products.Skip(currentPage * pageSize).Take(pageSize);
        }
    }
}