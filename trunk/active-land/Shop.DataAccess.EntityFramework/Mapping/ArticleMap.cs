using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Article", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.IsActive).HasColumnName("IsActive");


            // Ignored
            this.Ignore(t => t.Title);
            this.Ignore(t => t.Text);
            this.Ignore(t => t.Description);
            this.Ignore(t => t.IsCorrectLang);
            this.Ignore(t => t.CurrentLang);
        }
    }
}
