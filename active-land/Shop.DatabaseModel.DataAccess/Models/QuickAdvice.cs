using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class QuickAdvice
    {
        public QuickAdvice()
        {
            this.QuickAdviceLangs = new List<QuickAdviceLang>();
        }

        public int Id { get; set; }
        public bool Active { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<QuickAdviceLang> QuickAdviceLangs { get; set; }
    }
}
