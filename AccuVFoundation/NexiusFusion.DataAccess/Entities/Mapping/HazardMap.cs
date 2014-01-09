using System.Data.Entity.ModelConfiguration;

namespace NexiusFusion.DataAccess.Entities.Mapping
{
    public class HazardMap : EntityTypeConfiguration<Hazard>
    {
        public HazardMap()
        {
            // Primary Key
            this.HasKey(t => t.HazardId);

            // Properties
            this.Property(t => t.Type)
                .HasMaxLength(25);

            this.Property(t => t.HazardName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("dfr_hazard");
            this.Property(t => t.HazardId).HasColumnName("hazard_id");
            this.Property(t => t.Type).HasColumnName("type");
            this.Property(t => t.HazardName).HasColumnName("hazard_name");
        }
    }
}
