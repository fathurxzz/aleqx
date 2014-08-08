using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class Content
    {
        public Content()
        {
            this.ContentItems = new List<ContentItem>();
            this.ContentLangs = new List<ContentLang>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCatalogue { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<ContentItem> ContentItems { get; set; }
        public virtual ICollection<ContentLang> ContentLangs { get; set; }
    }
}
