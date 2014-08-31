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
        public IEnumerable<Subscriber> GetSubscribers()
        {
            return _store.Subscribers.ToList();
        }

        public Subscriber GetSubscriber(int id)
        {
            var article = _store.Subscribers.SingleOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new Exception(string.Format("Subscriber with id={0} not found", id));
            }
            return article;
        }

        public void DeleteSubscriber(int id)
        {
            var article = _store.Subscribers.SingleOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new Exception(string.Format("Subscriber with id={0} not found", id));
            }

            _store.Subscribers.Remove(article);
            _store.SaveChanges();
        }

        public void SaveSubscriber(Subscriber subscriber)
        {
            _store.SaveChanges();
        }

        public int AddSubscriber(Subscriber subscriber)
        {
            _store.Subscribers.Add(subscriber);
            _store.SaveChanges();
            return subscriber.Id;
        }
    }
}
