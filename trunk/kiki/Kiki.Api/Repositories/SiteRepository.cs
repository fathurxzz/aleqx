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

       

        public IEnumerable<Content> GetContents()
        {
            var contents = _store.Contents.ToList();
            return contents;
        }

        public Content GetContent(int id)
        {
            throw new NotImplementedException();
        }

        public Content GetContent(string name)
        {
            return _store.Contents.FirstOrDefault(c => c.Name == name);
        }

        public Content GetContent()
        {
            throw new NotImplementedException();
        }

        public void DeleteContent(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveContent(Content content)
        {
            throw new NotImplementedException();
        }

        public int AddContent(Content content)
        {
            throw new NotImplementedException();
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