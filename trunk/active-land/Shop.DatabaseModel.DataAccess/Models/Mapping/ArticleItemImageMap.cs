using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
{
    public class ArticleItemImageMap : EntityTypeConfiguration<ArticleItemImage>
    {
        public ArticleItemImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSource)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ArticleItemImage", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.ArticleItemId).HasColumnName("ArticleItemId");

            // Relationships
            this.HasRequired(t => t.ArticleItem)
                .WithMany(t => t.ArticleItemImages)
                .HasForeignKey(d => d.ArticleItemId);

        }
    }
}
