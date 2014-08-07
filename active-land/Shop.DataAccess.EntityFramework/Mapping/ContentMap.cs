using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ContentMap : EntityTypeConfiguration<Content>
    {
        public ContentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Content", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsCatalogue).HasColumnName("IsCatalogue");

            // Ignored
            this.Ignore(t => t.Title);
            this.Ignore(t => t.Text);
            this.Ignore(t => t.SeoDescription);
            this.Ignore(t => t.SeoKeywords);
            this.Ignore(t => t.SeoText);
            this.Ignore(t => t.IsCorrectLang);
            this.Ignore(t => t.CurrentLang);
        }
    }
}
