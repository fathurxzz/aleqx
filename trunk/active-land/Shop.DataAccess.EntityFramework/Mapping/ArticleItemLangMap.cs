using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ArticleItemLangMap : EntityTypeConfiguration<ArticleItemLang>
    {
        public ArticleItemLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(10000);

            // Table & Column Mappings
            this.ToTable("ArticleItemLang", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.ArticleItemId).HasColumnName("ArticleItemId");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");

            // Relationships
            this.HasRequired(t => t.ArticleItem)
                .WithMany(t => t.ArticleItemLangs)
                .HasForeignKey(d => d.ArticleItemId);
            this.HasRequired(t => t.Language)
                .WithMany(t => t.ArticleItemLangs)
                .HasForeignKey(d => d.LanguageId);

        }
    }
}
