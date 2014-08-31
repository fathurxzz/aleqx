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

       

       

        public IEnumerable<Reason> GetReasons()
        {
            throw new NotImplementedException();
        }

        public Content GetReason(int id)
        {
            throw new NotImplementedException();
        }

        public Content GetReason()
        {
            throw new NotImplementedException();
        }

        public void DeleteReason(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveReason(Reason content)
        {
            throw new NotImplementedException();
        }

        public int AddReason(Reason content)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Attention> GetAttentions()
        {
            throw new NotImplementedException();
        }

        public Content GetAttention(int id)
        {
            throw new NotImplementedException();
        }

        public Content GetAttention()
        {
            throw new NotImplementedException();
        }

        public void DeleteAttention(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveAttention(Attention content)
        {
            throw new NotImplementedException();
        }

        public int AddAttention(Attention content)
        {
            throw new NotImplementedException();
        }
    }
}