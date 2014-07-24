using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.Api.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly IShopStore _store;

        public CategoryRepository(IShopStore store)
        {
            _store = store;
        }

        public IEnumerable<Category> GetCategories()
        {
            try
            {
                var categories = _store.Categories.ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
