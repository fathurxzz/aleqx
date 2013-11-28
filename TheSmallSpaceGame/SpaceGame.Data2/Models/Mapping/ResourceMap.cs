using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SpaceGame.Data2.Models.Mapping
{
    public class ResourceMap : EntityTypeConfiguration<Resource>
    {
        public ResourceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Resource", "gbua_space");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.LastUpdate).HasColumnName("LastUpdate");
            this.Property(t => t.MineLevel).HasColumnName("MineLevel");
            this.Property(t => t.PlanetId).HasColumnName("PlanetId");
            this.Property(t => t.ResourceTypeId).HasColumnName("ResourceTypeId");

            // Relationships
            this.HasRequired(t => t.Planet)
                .WithMany(t => t.Resources)
                .HasForeignKey(d => d.PlanetId);
            this.HasRequired(t => t.ResourceType)
                .WithMany(t => t.Resources)
                .HasForeignKey(d => d.ResourceTypeId);

        }
    }
}
