using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.Api.Repositories
{
    public partial class ShopRepository : IShopRepository
    {
        public ProductStock GetProductStock(int id)
        {
            var productImage = _store.ProductStocks.SingleOrDefault(p => p.Id == id);
            if (productImage == null)
            {
                throw new Exception(string.Format("ProductStock with id={0} not found", id));
            }
            return productImage;
        }

        public void DeleteProductStock(int id)
        {
            //Log.DebugFormat("DeleteProductStock id:{0} started",id);
            var productImage = _store.ProductStocks.SingleOrDefault(p => p.Id == id);
            if (productImage == null)
            {
                //Log.ErrorFormat("ProductStock with id:{0} not found", id);
                throw new Exception(string.Format("ProductStock with id={0} not found", id));
            }
            _store.ProductStocks.Remove(productImage);
            _store.SaveChanges();
            //Log.DebugFormat("DeleteProductStock id:{0} finished", id);
        }

        public void SaveProductStock(ProductStock productStock)
        {
            _store.SaveChanges();
        }
    }
}
