using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;

namespace Kiki.Api.Repositories
{
    public partial class SiteRepository : ISiteRepository
    {
        public IEnumerable<Sale> GetSales()
        {
            return _store.Sales.ToList();
        }

        public Sale GetSale(int id)
        {
            var article = _store.Sales.SingleOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new Exception(string.Format("Sale with id={0} not found", id));
            }
            return article;
        }

        public void DeleteSale(int id, Action<string> deleteImages)
        {
            var article = _store.Sales.SingleOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new Exception(string.Format("Sale with id={0} not found", id));
            }

            deleteImages(article.ImageSource);
            _store.Sales.Remove(article);
            _store.SaveChanges();
        }

        public int AddSale(Sale sale)
        {
            _store.Sales.Add(sale);
            _store.SaveChanges();
            return sale.Id;
        }

        public void SaveSale(Sale sale)
        {
            _store.SaveChanges();
        }
    }
}
