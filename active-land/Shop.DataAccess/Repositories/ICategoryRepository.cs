using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        void UpdateCategory(int categoryId);
    }
}
