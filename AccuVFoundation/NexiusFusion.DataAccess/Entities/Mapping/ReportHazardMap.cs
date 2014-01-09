using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NexiusFusion.DataAccess.Entities.Mapping
{
    public class ReportHazardMap : EntityTypeConfiguration<ReportHazard>
    {
        public ReportHazardMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ReportId, t.HazardId });

            // Properties
            this.Property(t => t.ReportId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HazardId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("dfr_dfr_hazard");
            this.Property(t => t.ReportId).HasColumnName("dfr_id");
            this.Property(t => t.HazardId).HasColumnName("hazard_id");
            this.Property(t => t.LastModified).HasColumnName("last_modified");

            // Relationships
            this.HasRequired(t => t.DfrNexiusFusion)
                .WithMany(t => t.ReportHazards)
                .HasForeignKey(d => d.ReportId);
            this.HasRequired(t => t.Hazard)
                .WithMany(t => t.ReportHazards)
                .HasForeignKey(d => d.HazardId);

        }
    }
}
