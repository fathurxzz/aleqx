using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.Api.DataSynchronization.Model
{
    public static class ProductExtension
    {
        public static bool InjectFromImportProduct(this Product source, ImportedProduct importedProduct)
        {
            int changedFieldsCount = 0;
            if (source.ExternalId != importedProduct.ExternalId)
            {
                source.ExternalId = importedProduct.ExternalId;
                changedFieldsCount++;
            }

            if (source.Name != importedProduct.Name)
            {
                source.Name = importedProduct.Name;
                changedFieldsCount++;
            }
            if (source.Title != importedProduct.Title)
            {
                source.Title = importedProduct.Title;
                changedFieldsCount++;
            }
            if (source.OldPrice != importedProduct.OldPrice)
            {
                source.OldPrice = importedProduct.OldPrice;
                changedFieldsCount++;
            }

            if (source.Price != importedProduct.Price)
            {
                source.Price = importedProduct.Price;
                changedFieldsCount++;
            }
            if (source.IsNew != importedProduct.IsNew)
            {
                source.IsNew = importedProduct.IsNew;
                changedFieldsCount++;
            }
            if (source.IsActive != importedProduct.IsActive)
            {
                source.IsActive = importedProduct.IsActive;
                changedFieldsCount++;
            }
            if (source.IsDiscount != importedProduct.IsDiscount)
            {
                source.IsDiscount = importedProduct.IsDiscount;
                changedFieldsCount++;
            }
            if (source.IsTopSale != importedProduct.IsTopSale)
            {
                source.IsTopSale = importedProduct.IsTopSale;
                changedFieldsCount++;
            }

            return changedFieldsCount > 0;
        }
    }
}
