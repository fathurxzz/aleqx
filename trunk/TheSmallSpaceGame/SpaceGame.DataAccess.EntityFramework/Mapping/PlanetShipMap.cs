using System.Data.Entity.ModelConfiguration;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.DataAccess.EntityFramework.Mapping
{
    public class PlanetShipMap : EntityTypeConfiguration<PlanetShip>
    {
        public PlanetShipMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("PlanetShip", "gbua_space2");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.PlanetId).HasColumnName("PlanetId");
            this.Property(t => t.ShipId).HasColumnName("ShipId");

            // Relationships
            this.HasRequired(t => t.Planet)
                .WithMany(t => t.PlanetShips)
                .HasForeignKey(d => d.PlanetId);
            this.HasRequired(t => t.Ship)
                .WithMany(t => t.PlanetShips)
                .HasForeignKey(d => d.ShipId);

        }
    }
}