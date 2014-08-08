using System.Collections.Generic;

namespace Shop.DataAccess.Entities
{
    public partial class ContentItem
    {
        public ContentItem()
        {
            this.ContentItemImages = new List<ContentItemImage>();
            this.ContentItemLangs = new List<ContentItemLang>();
        }

        public int Id { get; set; }
        public int SortOrder { get; set; }
        public int ContentId { get; set; }
        public virtual Content Content { get; set; }
        public virtual ICollection<ContentItemImage> ContentItemImages { get; set; }
        public virtual ICollection<ContentItemLang> ContentItemLangs { get; set; }
    }
}
