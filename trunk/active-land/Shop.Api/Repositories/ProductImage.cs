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
        public ProductImage GetProductImage(int id)
        {
            var productImage = _store.ProductImages.SingleOrDefault(p => p.Id == id);
            if (productImage == null)
            {
                throw new Exception(string.Format("ProductImage with id={0} not found", id));
            }
            return productImage;
        }

        public void DeleteProductImage(int id, Action<String> deleteImage)
        {
            var productImage = _store.ProductImages.SingleOrDefault(p => p.Id == id);
            if (productImage == null)
            {
                throw new Exception(string.Format("ProductImage with id={0} not found", id));
            }
            deleteImage(productImage.ImageSource);
            _store.ProductImages.Remove(productImage);
            _store.SaveChanges();
        }

        public void SaveProductImage(ProductImage productImage)
        {
            _store.SaveChanges();
        }
    }
}
