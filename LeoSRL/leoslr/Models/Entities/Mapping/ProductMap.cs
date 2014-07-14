using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Leo.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Ignored
            this.Ignore(t => t.Title);
            this.Ignore(t => t.Text);
            this.Ignore(t => t.IsCorrectLang);
            this.Ignore(t => t.CurrentLang);

            // Table & Column Mappings
            this.ToTable("Product", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.IsContentPage).HasColumnName("ContentType");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
        }
    }
}
