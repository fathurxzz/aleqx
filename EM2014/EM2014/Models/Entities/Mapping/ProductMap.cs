using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EM2014.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSource)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Text)
                .HasMaxLength(1073741823);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Product", "gbua_em2014");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.ContentId).HasColumnName("ContentId");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.Title).HasColumnName("Title");

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.ContentId);

        }
    }
}
