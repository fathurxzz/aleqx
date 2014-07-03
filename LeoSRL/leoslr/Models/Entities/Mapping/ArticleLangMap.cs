using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Leo.Models.Mapping
{
    public class ArticleLangMap : EntityTypeConfiguration<ArticleLang>
    {
        public ArticleLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(1073741823);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("ArticleLang", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ArticleId).HasColumnName("ArticleId");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");

            // Relationships
            this.HasRequired(t => t.Article)
                .WithMany(t => t.ArticleLangs)
                .HasForeignKey(d => d.ArticleId);
            this.HasRequired(t => t.Language)
                .WithMany(t => t.ArticleLangs)
                .HasForeignKey(d => d.LanguageId);

        }
    }
}
