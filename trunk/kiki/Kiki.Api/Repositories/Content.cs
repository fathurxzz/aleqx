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
        public IEnumerable<Content> GetContents()
        {
            var contents = _store.Contents.ToList();
            return contents;
        }

        public Content GetContent(int id)
        {
            var content = _store.Contents.SingleOrDefault(c => c.Id == id);
            if (content == null)
            {
                throw new Exception(string.Format("Content with id={0} not found", id));
            }
            return content;
        }

        public Content GetContent(string name)
        {
            var content = _store.Contents.SingleOrDefault(c => c.Name == name);
            if (content == null)
            {
                return null;
                //throw new Exception(string.Format("Content with name={0} not found", name));
            }
            return content;
        }

        public Content GetContent()
        {
            var content = _store.Contents.FirstOrDefault(c => c.ContentType == 0);
            return content;
        }

    
        public void SaveContent(Content content)
        {
            var cache = _store.Contents.Single(c => c.Id == content.Id);
            _store.SaveChanges();
        }

      
    }
}
