using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
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

            this.Property(t => t.SearchCriteria)
              .IsRequired()
              .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("Product", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.SearchCriteria).HasColumnName("SearchCriteria");
            this.Property(t => t.IsNew).HasColumnName("IsNew");
            this.Property(t => t.IsDiscount).HasColumnName("IsDiscount");
            this.Property(t => t.IsTopSale).HasColumnName("IsTopSale");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.OldPrice).HasColumnName("OldPrice");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasMany(t => t.ProductAttributeValues)
                .WithMany(t => t.Products)
                .Map(m =>
                    {
                        m.ToTable("ProductAttributeValueProduct", "gbua_active_dev");
                        m.MapLeftKey("Products_Id");
                        m.MapRightKey("ProductAttributeValues_Id");
                    });

            this.HasRequired(t => t.Category)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.CategoryId);

            // Ignored
            this.Ignore(t => t.Title);
            this.Ignore(t => t.SeoDescription);
            this.Ignore(t => t.SeoKeywords);
            this.Ignore(t => t.SeoText);
            this.Ignore(t => t.Description);
            this.Ignore(t => t.IsCorrectLang);
            this.Ignore(t => t.CurrentLang);
            this.Ignore(t => t.ImageSource);
            this.Ignore(t => t.IsInCart);

        }
    }
}
