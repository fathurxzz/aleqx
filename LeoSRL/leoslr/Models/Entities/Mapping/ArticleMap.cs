using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Leo.Models.Mapping
{
    public class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Article", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Published).HasColumnName("Published");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");

            // Ignored
            this.Ignore(t => t.Title);
            this.Ignore(t => t.Description);
            this.Ignore(t => t.IsCorrectLang);
            this.Ignore(t => t.CurrentLang);
        }
    }
}
