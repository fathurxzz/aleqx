using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DeliveryService.DataAccess.Models.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Category", "gbua_delivery");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Title).HasColumnName("Title");

            // Relationships
            this.HasMany(t => t.Companies)
                .WithMany(t => t.Categories)
                .Map(m =>
                    {
                        m.ToTable("CompanyCategory", "gbua_delivery");
                        m.MapLeftKey("Categories_Id");
                        m.MapRightKey("Companies_Id");
                    });

            this.HasMany(t => t.Products)
                .WithMany(t => t.Categories)
                .Map(m =>
                    {
                        m.ToTable("ProductCategory", "gbua_delivery");
                        m.MapLeftKey("Category_Id");
                        m.MapRightKey("Product_Id");
                    });


        }
    }
}
