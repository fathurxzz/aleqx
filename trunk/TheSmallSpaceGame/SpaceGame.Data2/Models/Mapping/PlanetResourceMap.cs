using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SpaceGame.Data2.Models.Mapping
{
    public class PlanetResourceMap : EntityTypeConfiguration<PlanetResource>
    {
        public PlanetResourceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("PlanetResource", "gbua_space2");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.LastUpdate).HasColumnName("LastUpdate");
            this.Property(t => t.MineLevel).HasColumnName("MineLevel");
            this.Property(t => t.ResourceId).HasColumnName("ResourceId");
            this.Property(t => t.PlanetId).HasColumnName("PlanetId");

            // Relationships
            this.HasRequired(t => t.Planet)
                .WithMany(t => t.PlanetResources)
                .HasForeignKey(d => d.PlanetId);
            this.HasRequired(t => t.Resource)
                .WithMany(t => t.PlanetResources)
                .HasForeignKey(d => d.ResourceId);

        }
    }
}
