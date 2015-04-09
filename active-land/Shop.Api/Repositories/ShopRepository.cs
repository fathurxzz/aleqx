using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Shop.Api.DataSynchronization.Import;
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

        protected static readonly ILog Log = LogManager.GetLogger(typeof(ShopRepository));
    }
}
