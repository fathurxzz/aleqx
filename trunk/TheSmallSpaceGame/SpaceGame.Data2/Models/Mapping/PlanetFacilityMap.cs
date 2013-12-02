using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SpaceGame.Data2.Models.Mapping
{
    public class PlanetFacilityMap : EntityTypeConfiguration<PlanetFacility>
    {
        public PlanetFacilityMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("PlanetFacility", "gbua_space2");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.UpdateStart).HasColumnName("UpdateStart");
            this.Property(t => t.IsUpdating).HasColumnName("IsUpdating");
            this.Property(t => t.PlanetId).HasColumnName("PlanetId");
            this.Property(t => t.FacilityId).HasColumnName("FacilityId");

            // Relationships
            this.HasRequired(t => t.Facility)
                .WithMany(t => t.PlanetFacilities)
                .HasForeignKey(d => d.FacilityId);
            this.HasRequired(t => t.Planet)
                .WithMany(t => t.PlanetFacilities)
                .HasForeignKey(d => d.PlanetId);

        }
    }
}
