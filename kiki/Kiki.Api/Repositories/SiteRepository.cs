using System;
using System.Collections.Generic;
using System.Linq;
using Kiki.DataAccess;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;

namespace Kiki.Api.Repositories
{
    public partial class SiteRepository : ISiteRepository
    {
        private readonly ISiteStore _store;

        public SiteRepository(ISiteStore store)
        {
            _store = store;
        }
    }
}