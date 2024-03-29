using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
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

            // Table & Column Mappings
            this.ToTable("Product", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.Name).HasColumnName("Name");
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

        }
    }
}
