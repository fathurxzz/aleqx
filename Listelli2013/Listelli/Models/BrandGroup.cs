﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    public partial class BrandGroup
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
                var currentLang = BrandGroupLangs.FirstOrDefault(p => p.LanguageId == value);
                if (currentLang == null)
                {
                    IsCorrectLang = false;
                    var anyLang = BrandGroupLangs.FirstOrDefault();
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

        private void SetLang(BrandGroupLang postLang)
        {
            Title = postLang.Title;
            Description = postLang.Description;
        }
    }
}