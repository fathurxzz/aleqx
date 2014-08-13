using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.Api.Repositories
{
    public partial class ShopRepository : IShopRepository
    {
        public int AddQuickAdvice(QuickAdvice quickAdvice)
        {
            _store.QuickAdvices.Add(quickAdvice);

            CreateOrChangeEntityLanguage(quickAdvice);

            _store.SaveChanges();
            return quickAdvice.Id;
        }

        public IEnumerable<QuickAdvice> GetQuickAdvices(bool showOnlyActive = false)
        {
            var advices = _store.QuickAdvices.Where(a => a.Active || !showOnlyActive).ToList();
            foreach (var advice in advices)
            {
                advice.CurrentLang = LangId;
            }
            return advices;
        }

        public QuickAdvice GetQuickAdvice(int id)
        {
            var advice = _store.QuickAdvices.SingleOrDefault(a => a.Id == id);
            if (advice == null)
            {
                throw new Exception(string.Format("QuickAdvice with id={0} not found", id));
            }
            advice.CurrentLang = LangId;
            return advice;
        }

        public void DeleteQuickAdvice(int id)
        {
            var advice = _store.QuickAdvices.SingleOrDefault(a => a.Id == id);
            if (advice == null)
            {
                throw new Exception(string.Format("QuickAdvice with id={0} not found", id));
            }
            while (advice.QuickAdviceLangs.Any())
            {
                var adviceLang = advice.QuickAdviceLangs.First();
                _store.QuickAdviceLangs.Remove(adviceLang);
            }

            _store.QuickAdvices.Remove(advice);
            _store.SaveChanges();
        }

        public void SaveQuickAdvice(QuickAdvice quickAdvice)
        {
            var cache = _store.QuickAdvices.SingleOrDefault(a => a.Id == quickAdvice.Id);
            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        private void CreateOrChangeEntityLanguage(QuickAdvice cache)
        {
            var categoryLang = _store.QuickAdviceLangs.FirstOrDefault(r => r.QuickAdviceId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new QuickAdviceLang
                {
                    QuickAdviceId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                    Text = cache.Text,
                };
                _store.QuickAdviceLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
                categoryLang.Text = cache.Text;
            }

        }
    }
}
