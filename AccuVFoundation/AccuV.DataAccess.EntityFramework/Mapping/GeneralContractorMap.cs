using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class GeneralContractorMap : EntityTypeConfiguration<GeneralContractor>
    {

        public GeneralContractorMap()
        {
            // Primary Key
            this.HasKey(t => t.GcId);

            // Properties
            this.Property(t => t.GcUsername)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.GcName)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.GcTypeId)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("general_contractors");
            this.Property(t => t.GcId).HasColumnName("gc_id");
            this.Property(t => t.GcUsername).HasColumnName("gc_username");
            this.Property(t => t.GcName).HasColumnName("gc_name");
            this.Property(t => t.GcTypeId).HasColumnName("gc_type_id");
            this.Property(t => t.Active).HasColumnName("active");
            this.Property(t => t.LastModified).HasColumnName("last_modified");
        }
    }
}