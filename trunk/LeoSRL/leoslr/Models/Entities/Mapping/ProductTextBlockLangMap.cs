using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Leo.Models.Mapping
{
    public class ProductTextBlockLangMap : EntityTypeConfiguration<ProductTextBlockLang>
    {
        public ProductTextBlockLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            //this.Property(t => t.Text)
                //.IsRequired()
                //.HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("ProductTextBlockLang", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.ProductTextBlockId).HasColumnName("ProductTextBlockId");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");

            // Relationships
            this.HasRequired(t => t.ProductTextBlock)
                .WithMany(t => t.ProductTextBlockLangs)
                .HasForeignKey(d => d.ProductTextBlockId);
            this.HasRequired(t => t.Language)
                .WithMany(t => t.ProductTextBlockLangs)
                .HasForeignKey(d => d.LanguageId);
        }
    }
}