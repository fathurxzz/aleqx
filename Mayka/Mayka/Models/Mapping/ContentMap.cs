using System.Data.Entity.ModelConfiguration;
using Mayka.Models.Entities;

namespace Mayka.Models.Mapping
{
    public class ContentMap:EntityTypeConfiguration<Content>
    {
        public ContentMap()
        {
            this.HasKey(t => t.Id);

            this.Property(t => t.Name).IsRequired().HasMaxLength(255);
            this.Property(t => t.Title).IsRequired().HasMaxLength(255);
            this.Property(t => t.MenuTitle).IsRequired().HasMaxLength(255);
            this.Property(t => t.SortOrder).IsRequired();
            this.Property(t => t.MainPage).IsRequired();

            this.ToTable("Content", "SiteContainer");
        }
    }
}