using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
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

            this.Property(t => t.CategoryLevel)
                .IsRequired()
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("Category", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.CategoryLevel).HasColumnName("CategoryLevel");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");

            // Relationships
            this.HasMany(t => t.ProductAttributes)
                .WithMany(t => t.Categories)
                .Map(m =>
                    {
                        m.ToTable("CategoryProductAttribute", "gbua_active_dev");
                        m.MapLeftKey("Categories_Id");
                        m.MapRightKey("ProductAttributes_Id");
                    });

            this.HasOptional(t => t.Parent)
                .WithMany(t => t.Children)
                .HasForeignKey(d => d.CategoryId);

        }
    }
}
