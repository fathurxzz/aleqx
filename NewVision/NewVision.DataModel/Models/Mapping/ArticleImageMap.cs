using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NewVision.DataModel.Models.Mapping
{
    public class ArticleImageMap : EntityTypeConfiguration<ArticleImage>
    {
        public ArticleImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSrc)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ArticleImage", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
            this.Property(t => t.ArticleId).HasColumnName("ArticleId");

            // Relationships
            this.HasRequired(t => t.Article)
                .WithMany(t => t.ArticleImages)
                .HasForeignKey(d => d.ArticleId);

        }
    }
}
