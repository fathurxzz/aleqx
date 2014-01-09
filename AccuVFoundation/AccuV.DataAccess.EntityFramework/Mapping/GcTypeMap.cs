using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class GcTypeMap : EntityTypeConfiguration<GCType>
    {

        public GcTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.GcTypeId);

            // Properties
            this.Property(t => t.GcTypeId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.GcTypeDesc)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("gc_type");
            this.Property(t => t.GcTypeId).HasColumnName("gc_type_id");
            this.Property(t => t.GcTypeDesc).HasColumnName("gc_type_desc");
        }
    }
}