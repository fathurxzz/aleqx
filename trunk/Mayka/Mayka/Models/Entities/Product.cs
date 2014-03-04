using System.Collections.Generic;

namespace Mayka.Models.Entities
{
    public partial class Product
    {
        public Product()
        {
            this.ProductImages = new HashSet<ProductImage>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string PreviewImageSource { get; set; }
        public int ContentId { get; set; }

        public virtual Content Content { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
