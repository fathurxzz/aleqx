using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FashionIntention.UI.Models.Mapping
{
    public class TagMap : EntityTypeConfiguration<Tag>
    {
        public TagMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Tag", "gbua_fashint");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
        }
    }
}
