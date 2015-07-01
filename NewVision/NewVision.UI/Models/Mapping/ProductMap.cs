using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NewVision.UI.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Price)
                .HasMaxLength(10);

            this.Property(t => t.ImageSrc)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Title)
                .HasMaxLength(100);

            this.Property(t => t.TitleEn)
                .HasMaxLength(100);

            this.Property(t => t.TitleUa)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Product", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AuthorId).HasColumnName("AuthorId");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.ViewDate).HasColumnName("ViewDate");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TitleEn).HasColumnName("TitleEn");
            this.Property(t => t.TitleUa).HasColumnName("TitleUa");

            // Relationships
            this.HasMany(t => t.Tags)
                .WithMany(t => t.Products)
                .Map(m =>
                    {
                        m.ToTable("ProductTag", "gbua_new_vision");
                        m.MapLeftKey("Products_Id");
                        m.MapRightKey("Tags_Id");
                    });

            this.HasRequired(t => t.Author)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.AuthorId);

        }
    }
}
