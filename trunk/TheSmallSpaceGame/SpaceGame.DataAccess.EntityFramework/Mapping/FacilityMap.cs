using System.Data.Entity.ModelConfiguration;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.DataAccess.EntityFramework.Mapping
{
    public class FacilityMap : EntityTypeConfiguration<Facility>
    {
        public FacilityMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(65535);

            // Table & Column Mappings
            this.ToTable("Facility", "gbua_space2");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.MetalCost).HasColumnName("MetalCost");
            this.Property(t => t.CrystalCost).HasColumnName("CrystalCost");
            this.Property(t => t.DeiteriumCost).HasColumnName("DeiteriumCost");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}