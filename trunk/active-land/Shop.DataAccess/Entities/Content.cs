using System.Collections.Generic;

namespace Shop.DataAccess.Entities
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
        public int ContentType { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<ContentItem> ContentItems { get; set; }
        public virtual ICollection<ContentLang> ContentLangs { get; set; }
    }
}
