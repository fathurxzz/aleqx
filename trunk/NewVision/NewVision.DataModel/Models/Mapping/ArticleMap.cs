using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NewVision.DataModel.Models.Mapping
{
    public class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(500);

            this.Property(t => t.Text)
                .HasMaxLength(1073741823);

            this.Property(t => t.ImageSrc)
                .HasMaxLength(500);

            this.Property(t => t.VideoSrc)
                .HasMaxLength(500);

            this.Property(t => t.TitleEn)
                .HasMaxLength(500);

            this.Property(t => t.TitleUa)
                .HasMaxLength(500);

            this.Property(t => t.TextEn)
                .HasMaxLength(1073741823);

            this.Property(t => t.TextUa)
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("Article", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.TitlePosition).HasColumnName("TitlePosition");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
            this.Property(t => t.VideoSrc).HasColumnName("VideoSrc");
            this.Property(t => t.TitleEn).HasColumnName("TitleEn");
            this.Property(t => t.TitleUa).HasColumnName("TitleUa");
            this.Property(t => t.TextEn).HasColumnName("TextEn");
            this.Property(t => t.TextUa).HasColumnName("TextUa");
        }
    }
}
