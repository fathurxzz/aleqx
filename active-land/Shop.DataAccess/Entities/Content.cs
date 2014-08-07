using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Entities
{
    public partial class Content
    {
        public Content()
        {
            this.ContentLangs = new List<ContentLang>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCatalogue { get; set; }
        public virtual ICollection<ContentLang> ContentLangs { get; set; }
    }
}
