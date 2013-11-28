using System.Data.Entity.ModelConfiguration;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.DataAccess.EntityFramework.Mapping
{
    public class PlanetMap : EntityTypeConfiguration<Planet>
    {
        public PlanetMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Planet", "gbua_space");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.UserId).HasColumnName("UserId");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Planets)
                .HasForeignKey(d => d.UserId);

        }
    }
}
