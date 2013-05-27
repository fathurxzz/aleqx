using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    public partial class Article
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsCorrectLang { get; protected set; }

        private int _currentLang;

        public int CurrentLang
        {
            get
            {
                return _currentLang;
            }

            set
            {
                _currentLang = value;
                var currentLang = ArticleLangs.FirstOrDefault(p => p.LanguageId == value);
                if (currentLang == null)
                {
                    IsCorrectLang = false;
                    var anyLang = ArticleLangs.FirstOrDefault();
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

        private void SetLang(ArticleLang postLang)
        {
            Title = postLang.Title;
            Description = postLang.Description;
        }
    }
}