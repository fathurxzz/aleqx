using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Models
{
    public class CartModel:SiteModel
    {
        public Order Order { get; set; }
        public OrderComplete OrderComplete { get; set; }

        public CartModel(IShopRepository repository, int langId, string contentName) : base(repository,langId, contentName)
        {
            
        }
    }
}