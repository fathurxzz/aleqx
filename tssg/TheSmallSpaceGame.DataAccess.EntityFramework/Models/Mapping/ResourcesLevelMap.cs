using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TheSmallSpaceGame.DataAccess.Entities;

namespace TheSmallSpaceGame.DataAccess.EntityFramework.Models.Mapping
{
    public class ResourcesLevelMap : EntityTypeConfiguration<ResourcesLevel>
    {
        public ResourcesLevelMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ResourcesLevel", "gbua_sgame");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MetalLevel).HasColumnName("MetalLevel");
            this.Property(t => t.CrystalLevel).HasColumnName("CrystalLevel");
            this.Property(t => t.DeiteriumLevel).HasColumnName("DeiteriumLevel");
            this.Property(t => t.User_Id).HasColumnName("User_Id");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.ResourcesLevels)
                .HasForeignKey(d => d.User_Id);

        }
    }
}
