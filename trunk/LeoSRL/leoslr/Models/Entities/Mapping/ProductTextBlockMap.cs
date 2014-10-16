using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Leo.Models.Mapping
{
    public class ProductTextBlockMap : EntityTypeConfiguration<ProductTextBlock>
    {
        public ProductTextBlockMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductTextBlock", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.ProductId).HasColumnName("ProductId");

            // Ignored
            this.Ignore(t => t.Text);
            this.Ignore(t => t.Title);
            this.Ignore(t => t.IsCorrectLang);
            this.Ignore(t => t.CurrentLang);

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductTextBlocks)
                .HasForeignKey(d => d.ProductId);
        }
    }
}