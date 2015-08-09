using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NewVision.UI.Models.Mapping
{
    public class AuthorMap : EntityTypeConfiguration<Author>
    {
        public AuthorMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Photo)
                .HasMaxLength(500);

            this.Property(t => t.Avatar)
                .HasMaxLength(500);

            this.Property(t => t.Title)
                .HasMaxLength(100);

            this.Property(t => t.TitleEn)
                .HasMaxLength(100);

            this.Property(t => t.TitleUa)
                .HasMaxLength(100);

            this.Property(t => t.About)
                .HasMaxLength(1073741823);

            this.Property(t => t.AboutEn)
                .HasMaxLength(1073741823);

            this.Property(t => t.AboutUa)
                .HasMaxLength(1073741823);

            this.Property(t => t.Description)
                .HasMaxLength(1073741823);

            this.Property(t => t.DescriptionEn)
                .HasMaxLength(1073741823);

            this.Property(t => t.DescriptionUa)
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("Author", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Photo).HasColumnName("Photo");
            this.Property(t => t.Avatar).HasColumnName("Avatar");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TitleEn).HasColumnName("TitleEn");
            this.Property(t => t.TitleUa).HasColumnName("TitleUa");
            this.Property(t => t.About).HasColumnName("About");
            this.Property(t => t.AboutEn).HasColumnName("AboutEn");
            this.Property(t => t.AboutUa).HasColumnName("AboutUa");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DescriptionEn).HasColumnName("DescriptionEn");
            this.Property(t => t.DescriptionUa).HasColumnName("DescriptionUa");

            // Relationships
            this.HasMany(t => t.Events)
                .WithMany(t => t.Authors)
                .Map(m =>
                {
                    m.ToTable("AuthorEvent", "gbua_new_vision");
                    m.MapLeftKey("Authors_Id");
                    m.MapRightKey("Events_Id");
                });

            this.HasMany(t => t.Tags)
                .WithMany(t => t.Authors)
                .Map(m =>
                    {
                        m.ToTable("AuthorTag", "gbua_new_vision");
                        m.MapLeftKey("Authors_Id");
                        m.MapRightKey("Tags_Id");
                    });


        }
    }
}
