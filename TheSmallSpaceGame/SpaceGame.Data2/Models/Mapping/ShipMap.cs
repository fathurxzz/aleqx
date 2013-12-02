using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SpaceGame.Data2.Models.Mapping
{
    public class ShipMap : EntityTypeConfiguration<Ship>
    {
        public ShipMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Ship", "gbua_space2");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Health).HasColumnName("Health");
            this.Property(t => t.Offence).HasColumnName("Offence");
            this.Property(t => t.Capacity).HasColumnName("Capacity");
            this.Property(t => t.Deffence).HasColumnName("Deffence");
        }
    }
}
