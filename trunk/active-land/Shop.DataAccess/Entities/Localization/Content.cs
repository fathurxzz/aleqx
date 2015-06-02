using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    partial class Content
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoText { get; set; }
        public bool IsCorrectLang { get; protected set; }

        public bool IsMainPage {
            get { return ContentType == 0; }
        }

        private int _currentLang;

        //private IEnumerable<ContentLang> ContentLangsCached
        //{
        //    get { return (ICollection<ContentLang>)Cache.Default.GetOrAdd("ContentLangsCached_" + Id, key => (object)ContentLangs.ToList()); }
        //}

        public int CurrentLang
        {
            get
            {
                return _currentLang;
            }

            set
            {
                _currentLang = value;
                //var currentLang = ContentLangsCached.FirstOrDefault(c => c.LanguageId == value);
                var currentLang = ContentLangs.FirstOrDefault(c => c.LanguageId == value);
                if (currentLang == null)
                {
                    IsCorrectLang = false;
                    //var anyLang = ContentLangsCached.FirstOrDefault();
                    var anyLang = ContentLangs.FirstOrDefault();
                    if (anyLang != null)
                    {
                        SetLang(anyLang);
                    }
                }
                else
                {
                    IsCorrectLang = true;
                    SetLang(currentLang);
                }
            }
        }

        private void SetLang(ContentLang entityLang)
        {
            Title = entityLang.Title;
            Text = entityLang.Text;
            SeoDescription = entityLang.SeoDescription;
            SeoKeywords = entityLang.SeoKeywords;
            SeoText = entityLang.SeoText;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
