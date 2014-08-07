using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
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
        }
    }
}
