using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class GcSiteMap : EntityTypeConfiguration<GCSite>
    {
        public GcSiteMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SiteNumber, t.ProjectId, t.GcTypeId, t.GcId });

            // Properties
            this.Property(t => t.SiteNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProjectId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.GcTypeId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.GcId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("gc_site");
            this.Property(t => t.SiteNumber).HasColumnName("site_number");
            this.Property(t => t.ProjectId).HasColumnName("project_id");
            this.Property(t => t.GcTypeId).HasColumnName("gc_type_id");
            this.Property(t => t.GcId).HasColumnName("gc_id");
            this.Property(t => t.Active).HasColumnName("active");
            this.Property(t => t.LastModified).HasColumnName("last_modified");

            // Relationships
            this.HasRequired(t => t.GeneralContractor)
                .WithMany(t => t.GcSite)
                .HasForeignKey(d => d.GcId);

        }
    }
}
