using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class ActivityMap : EntityTypeConfiguration<Activity>
    {
        public ActivityMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ActivityId, t.ActivityTypeId });

            // Properties
            this.Property(t => t.ActivityDescription)
                .HasMaxLength(100);

            this.Property(t => t.ActivityTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ModuleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


            // Table & Column Mappings
            this.ToTable("sec_activity");
            this.Property(t => t.ActivityId).HasColumnName("activity_id");
            this.Property(t => t.ActivityDescription).HasColumnName("activity_description");
            this.Property(t => t.ActivityTypeId).HasColumnName("type_activity_id");
            this.Property(t => t.Active).HasColumnName("active");
            this.Property(t => t.ModuleId).HasColumnName("module_id");

            // Relationships
            this.HasMany(t => t.Sites)
                .WithMany(t => t.Activities)
                .Map(m =>
                {
                    m.ToTable("sec_site_activity");
                    m.MapLeftKey("activity_id", "type_activity_id");
                    m.MapRightKey("site_number", "project_id");
                });

            this.HasRequired(t => t.ActivityType)
                .WithMany(t => t.Activities)
                .HasForeignKey(d => d.ActivityTypeId);

            this.HasRequired(t => t.Module)
                .WithMany(t => t.Activities)
                .HasForeignKey(d => d.ModuleId);
        }
    }
}
