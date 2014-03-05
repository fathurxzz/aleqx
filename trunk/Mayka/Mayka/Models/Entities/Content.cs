using System.Collections.Generic;
using Mayka.Models.Entities;

namespace Mayka.Models
{
    public partial class Content
    {
        public Content()
        {
            this.PhotoGalleryItems = new List<PhotoGalleryItem>();
            this.Products = new List<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string MenuTitle { get; set; }
        public int SortOrder { get; set; }
        public string Text { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public byte ContentType { get; set; }
        public virtual ICollection<PhotoGalleryItem> PhotoGalleryItems { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
