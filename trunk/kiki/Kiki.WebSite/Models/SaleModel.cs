using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;

namespace Kiki.WebSite.Models
{
    public class SaleModel:SiteModel
    {
        public Sale Sale { get; set; }

        public SaleModel(ISiteRepository repository, string contentName, string saleId) : base(repository, contentName)
        {
            Sale = Sales.First(a => a.Name == saleId);
        }


    }
}