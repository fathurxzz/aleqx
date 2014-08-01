using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
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
                .IsRequired();
                //.HasMaxLength(1073741823);

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


            // Ignored
            this.Ignore(t => t.Title);
            this.Ignore(t => t.SeoDescription);
            this.Ignore(t => t.SeoKeywords);
            this.Ignore(t => t.SeoText);
            this.Ignore(t => t.IsCorrectLang);
            this.Ignore(t => t.CurrentLang);
            this.Ignore(t => t.Selected);

        }
    }
}
