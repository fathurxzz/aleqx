using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leo.Models
{
    public partial class SpecialContent
    {
        public string Title { get; set; }
        public string Text { get; set; }
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
                var currentLang = SpecialContentLangs.FirstOrDefault(p => p.LanguageId == value);
                if (currentLang == null)
                {
                    IsCorrectLang = false;
                    var anyLang = SpecialContentLangs.FirstOrDefault();
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

        private void SetLang(SpecialContentLang postLang)
        {
            Title = postLang.Title;
            Text = postLang.Text;
        }
    }
}