using System.Collections.Generic;
using System.Linq;
using Kiki.DataAccess;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;

namespace Kiki.Api.Repositories
{
    public class SiteRepository : ISiteRepository
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

        public Content GetContent(string name)
        {
            return _store.Contents.FirstOrDefault(c => c.Name == name);
        }
    }
}