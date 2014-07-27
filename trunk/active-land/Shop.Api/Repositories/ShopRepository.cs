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
    public class ShopRepository:IShopRepository
    {
        private IShopStore _store;

        public ShopRepository(IShopStore store)
        {
            _store = store;
        }

        public IEnumerable<Category> GetCategories(int langId)
        {
            var categories = _store.Categories;
            foreach (var category in categories)
            {
                category.CurrentLang = langId;
            }
            return categories;
        }

        public void UpdateCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
