using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class RolloutTrackingMap : EntityTypeConfiguration<RolloutTracking>
    {
        public RolloutTrackingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SiteNumber, t.TaskId, t.ProjectId });

            // Properties
            this.Property(t => t.SiteNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TaskId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ProjectId)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Rollout Tracking");
            this.Property(t => t.SiteNumber).HasColumnName("Site Number");
            this.Property(t => t.TaskId).HasColumnName("Task ID");
            this.Property(t => t.ProjectId).HasColumnName("Project ID");
            this.Property(t => t.CompletedDate).HasColumnName("Completed Date");
            this.Property(t => t.ForecastDate).HasColumnName("Forecast Date");
            this.Property(t => t.LastModified).HasColumnName("last_modified");

            // Relationships
            this.HasRequired(t => t.Site)
                .WithMany(t => t.RolloutTracking)
                .HasForeignKey(d => new { d.SiteNumber, d.ProjectId });
            this.HasRequired(t => t.Task)
                .WithMany(t => t.RolloutTracking)
                .HasForeignKey(d => new { d.TaskId, d.ProjectId });

        }
    }
}
