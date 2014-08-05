using System;
using System.Collections.Generic;
using System.Linq;
using Shop.DataAccess;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.Api.Repositories
{
    public partial class ShopRepository : IShopRepository
    {
        private readonly IShopStore _store;
        public ShopRepository(IShopStore store)
        {
            _store = store;
        }
        public int LangId { get; set; }
    }
}
