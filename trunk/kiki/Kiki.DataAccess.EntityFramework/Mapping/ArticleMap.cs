using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.EntityFramework.Mapping
{
    public class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            this.Property(t => t.TitleEng)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            this.Property(t => t.ImageSource)
                .IsRequired()
                .HasMaxLength(200);
            
            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(10000);
            
            this.Property(t => t.TextEng)
                .IsRequired()
                .HasMaxLength(10000);

            // Table & Column Mappings
            this.ToTable("Article", "gbua_kiki");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TitleEng).HasColumnName("TitleEng");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.TextEng).HasColumnName("TextEng");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
        }
    }
}
