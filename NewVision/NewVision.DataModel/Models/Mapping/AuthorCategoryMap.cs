using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NewVision.DataModel.Models.Mapping
{
    public class AuthorCategoryMap : EntityTypeConfiguration<AuthorCategory>
    {
        public AuthorCategoryMap()
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
            this.ToTable("AuthorCategory", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TitleEn).HasColumnName("TitleEn");
            this.Property(t => t.TitleUa).HasColumnName("TitleUa");

            // Relationships
            this.HasMany(t => t.Categories)
                .WithMany(t => t.AuthorCategories)
                .Map(m =>
                    {
                        m.ToTable("AuthorCategoryCategory", "gbua_new_vision");
                        m.MapLeftKey("AuthorCategories_Id");
                        m.MapRightKey("Categories_Id");
                    });

            this.HasMany(t => t.Tags)
                .WithMany(t => t.AuthorCategories)
                .Map(m =>
                    {
                        m.ToTable("AuthorCategoryTag", "gbua_new_vision");
                        m.MapLeftKey("AuthorCategories_Id");
                        m.MapRightKey("Tags_Id");
                    });


        }
    }
}
