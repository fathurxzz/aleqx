using System.Data.Entity.ModelConfiguration;

namespace NexiusFusion.DataAccess.Entities.Mapping
{
    public class StructureTypeMap : EntityTypeConfiguration<StructureType>
    {
        public StructureTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.StructureTypeId);

            // Properties
            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("dfr_structure_type");
            this.Property(t => t.StructureTypeId).HasColumnName("structure_type_id");
            this.Property(t => t.Type).HasColumnName("structure_type");
        }
    }
}
