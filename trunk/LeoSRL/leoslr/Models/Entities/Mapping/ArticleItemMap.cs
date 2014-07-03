using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Leo.Models.Mapping
{
    public class ArticleItemMap : EntityTypeConfiguration<ArticleItem>
    {
        public ArticleItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ArticleItem", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.ArticleId).HasColumnName("ArticleId");
            this.Property(t => t.ContentType).HasColumnName("ContentType");

            // Ignored
            this.Ignore(t => t.Text);
            this.Ignore(t => t.IsCorrectLang);
            this.Ignore(t => t.CurrentLang);

            // Relationships
            this.HasRequired(t => t.Article)
                .WithMany(t => t.ArticleItems)
                .HasForeignKey(d => d.ArticleId);

        }
    }
}
