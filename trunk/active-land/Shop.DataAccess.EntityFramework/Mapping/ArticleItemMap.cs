using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ArticleItemMap : EntityTypeConfiguration<ArticleItem>
    {
        public ArticleItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ArticleItem", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.ArticleId).HasColumnName("ArticleId");

            // Relationships
            this.HasRequired(t => t.Article)
                .WithMany(t => t.ArticleItems)
                .HasForeignKey(d => d.ArticleId);

        }
    }
}
