using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Leo.Models.Mapping
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
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ArticleItemImage", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
            this.Property(t => t.ArticleItemId).HasColumnName("ArticleItemId");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");

            // Relationships
            this.HasRequired(t => t.ArticleItem)
                .WithMany(t => t.ArticleItemImages)
                .HasForeignKey(d => d.ArticleItemId);

        }
    }
}
