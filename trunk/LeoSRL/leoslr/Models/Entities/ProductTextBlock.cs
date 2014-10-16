using System.Collections.Generic;


namespace Leo.Models
{
    public partial class ProductTextBlock
    {
        public ProductTextBlock()
        {
            this.ProductTextBlockFiles = new List<ProductTextBlockFile>();
            this.ProductTextBlockLangs = new List<ProductTextBlockLang>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SortOrder { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductTextBlockFile> ProductTextBlockFiles { get; set; }
        public virtual ICollection<ProductTextBlockLang> ProductTextBlockLangs { get; set; }

    }
}