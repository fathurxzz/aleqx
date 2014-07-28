using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework
{
    public class ShopStore : IShopStore
    {
        readonly ShopContext _context = new ShopContext();


        public IDbSet<Category> Categories
        {
            get { return _context.Categories; }
        }

        public IDbSet<CategoryLang> CategoryLangs
        {
            get { return _context.CategoryLangs; }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        
    }
}
