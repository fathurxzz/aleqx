using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
{
    public class ContentItemMap : EntityTypeConfiguration<ContentItem>
    {
        public ContentItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ContentItem", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.ContentId).HasColumnName("ContentId");

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.ContentItems)
                .HasForeignKey(d => d.ContentId);

        }
    }
}
