using System.Data.Entity.ModelConfiguration;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.DataAccess.EntityFramework.Mapping
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
            this.Property(t => t.UpdateFinish).HasColumnName("UpdateFinish");
            this.Property(t => t.IsUpdating).HasColumnName("IsUpdating");
            this.Property(t => t.PlanetId).HasColumnName("PlanetId");
            this.Property(t => t.FacilityId).HasColumnName("FacilityId");
            
            // Not mapped properties
            this.Ignore(t=>t.IsAvailableForUpgrade);
            this.Ignore(t => t.UpgradeFacilityCostMetal);
            this.Ignore(t => t.UpgradeFacilityCostCrystal);
            this.Ignore(t => t.UpgradeFacilityCostDeiterium);

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