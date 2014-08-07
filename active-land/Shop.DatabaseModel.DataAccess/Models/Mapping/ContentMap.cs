using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
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
        }
    }
}
