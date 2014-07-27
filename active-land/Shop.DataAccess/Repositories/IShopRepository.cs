using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.Repositories
{
    public interface IShopRepository:IRepository
    {
        IEnumerable<Category> GetCategories(int currentLangId);
        void UpdateCategory(int categoryId);
    }
}
