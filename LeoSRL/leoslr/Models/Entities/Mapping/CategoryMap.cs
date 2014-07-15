using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Leo.Models.Mapping
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

            // Table & Column Mappings
            this.ToTable("Category", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.IsNewsCategory).HasColumnName("CategoryType");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.CategoryLevel).HasColumnName("CategoryLevel");

            // Ignored
            this.Ignore(t => t.Title);
            this.Ignore(t => t.Text);
            this.Ignore(t => t.IsCorrectLang);
            this.Ignore(t => t.CurrentLang);
            this.Ignore(t => t.HasContentPages);

            // Relationships
            this.HasOptional(t => t.Parent)
                .WithMany(t => t.Children)
                .HasForeignKey(d => d.CategoryId);

        }
    }
}
