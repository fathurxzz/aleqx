using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess
{
    public interface IShopStore
    {
        IDbSet<Category> Categories { get; }
        IDbSet<CategoryLang> CategoryLangs { get; }


        int SaveChanges();
    }
}
