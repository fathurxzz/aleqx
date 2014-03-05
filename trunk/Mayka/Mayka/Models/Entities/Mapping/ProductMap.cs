using System.Data.Entity.ModelConfiguration;

namespace Mayka.Models.Entities
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(65535);

            this.Property(t => t.PreviewImageSource)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Product", "gbua_mayka");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.PreviewImageSource).HasColumnName("PreviewImageSource");
            this.Property(t => t.ContentId).HasColumnName("ContentId");

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.ContentId);
        }
    }
}