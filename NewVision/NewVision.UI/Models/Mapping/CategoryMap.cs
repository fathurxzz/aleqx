using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NewVision.UI.Models.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(100);

            this.Property(t => t.TitleEn)
                .HasMaxLength(100);

            this.Property(t => t.TitleUa)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Category", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TitleEn).HasColumnName("TitleEn");
            this.Property(t => t.TitleUa).HasColumnName("TitleUa");

            // Relationships
            this.HasMany(t => t.Tags)
                .WithMany(t => t.Categories)
                .Map(m =>
                    {
                        m.ToTable("CategoryTag", "gbua_new_vision");
                        m.MapLeftKey("Categories_Id");
                        m.MapRightKey("Tags_Id");
                    });


        }
    }
}
