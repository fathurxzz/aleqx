using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FashionIntention.UI.Models.Mapping
{
    public class ArticleItemMap : EntityTypeConfiguration<ArticleItem>
    {
        public ArticleItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSrc)
                .HasMaxLength(500);

            this.Property(t => t.Text)
                .HasMaxLength(1073741823);

            this.Property(t => t.VideoSrc)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ArticleItem", "gbua_fashint");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.ArticleId).HasColumnName("ArticleId");
            this.Property(t => t.VideoSrc).HasColumnName("VideoSrc");

            // Relationships
            this.HasRequired(t => t.Article)
                .WithMany(t => t.ArticleItems)
                .HasForeignKey(d => d.ArticleId);

        }
    }
}
