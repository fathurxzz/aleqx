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
              .HasMaxLength(2000);
            
            this.Property(t => t.SearchCriteriaAttributes)
              .IsRequired()
              .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("Product", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.SearchCriteria).HasColumnName("SearchCriteria");
            this.Property(t => t.SearchCriteriaAttributes).HasColumnName("SearchCriteriaAttributes");
            this.Property(t => t.ExternalId).HasColumnName("ExternalId");
            this.Property(t => t.OriginalUrl).HasColumnName("OriginalUrl");
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

            this.HasMany(t => t.ProductChildren)
                .WithMany(t => t.ProductParents)
                .Map(m =>
                {
                    m.ToTable("ProductProduct", "gbua_active_dev");
                    m.MapLeftKey("ProductParent_Id");
                    m.MapRightKey("ProductChildren_Id");
                });

            this.HasMany(t => t.ProductParents)
               .WithMany(t => t.ProductChildren)
               .Map(m =>
               {
                   m.ToTable("ProductProduct", "gbua_active_dev");
                   m.MapLeftKey("ProductChildren_Id");
                   m.MapRightKey("ProductParent_Id");
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
            this.Ignore(t => t.IsSelectedByFilter);

        }
    }
}
