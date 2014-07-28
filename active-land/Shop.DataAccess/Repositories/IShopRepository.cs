using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.Repositories
{
    public interface IShopRepository : IRepository
    {
        int LangId { get; set; }
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        int AddCategory(Category category);
        
        void Save(Category category);
    }
}
