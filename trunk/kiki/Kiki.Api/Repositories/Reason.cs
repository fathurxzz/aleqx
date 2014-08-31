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
        public IEnumerable<Reason> GetReasons()
        {
            return _store.Reasons.ToList();
        }

        public Reason GetReason(int id)
        {
            var article = _store.Reasons.SingleOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new Exception(string.Format("Reason with id={0} not found", id));
            }
            return article;
        }

        public void DeleteReason(int id)
        {
            var article = _store.Reasons.SingleOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new Exception(string.Format("Reason with id={0} not found", id));
            }

            _store.Reasons.Remove(article);
            _store.SaveChanges();
        }

        public void SaveReason(Reason reason)
        {
            //var cache = _store.Reasons.SingleOrDefault(a => a.Id == reason.Id);
            _store.SaveChanges();
        }

        public int AddReason(Reason reason)
        {
            _store.Reasons.Add(reason);
            _store.SaveChanges();
            return reason.Id;
        }
    }
}
