using System;
using System.Collections.Generic;

namespace Leo.Models
{
    public partial class SpecialContent
    {
        public SpecialContent()
        {
            this.SpecialContentLangs = new List<SpecialContentLang>();
        }

        public int Id { get; set; }
        public string PageImageSource { get; set; }
        public string ContentImageSource { get; set; }
        public bool IsFirstCategory { get; set; }
        public bool IsSecondCategory { get; set; }
        public virtual ICollection<SpecialContentLang> SpecialContentLangs { get; set; }
    }
}
