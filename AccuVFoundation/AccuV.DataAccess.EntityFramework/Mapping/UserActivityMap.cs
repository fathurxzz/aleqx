using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class UserActivityMap : EntityTypeConfiguration<UserActivity>
    {
        public UserActivityMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UsrSid, t.ActivityId, t.ActivityTypeId });

            // Properties
            this.Property(t => t.UsrSid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ActivityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ActivityTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("sec_user_activity");
            this.Property(t => t.UsrSid).HasColumnName("usr_sid");
            this.Property(t => t.ActivityId).HasColumnName("activity_id");
            this.Property(t => t.ActivityTypeId).HasColumnName("type_activity_id");
            this.Property(t => t.AccessCreate).HasColumnName("access_create");
            this.Property(t => t.AccessRead).HasColumnName("access_read");
            this.Property(t => t.AccessUpdate).HasColumnName("access_update");
            this.Property(t => t.AccessDelete).HasColumnName("access_delete");
            this.Property(t => t.AccessApprove).HasColumnName("access_approve");
            this.Property(t => t.AccessAdmin).HasColumnName("access_admin");

            // Relationships
            this.HasRequired(t => t.Activity)
                .WithMany(t => t.UserActivities)
                .HasForeignKey(d => new { d.ActivityId, d.ActivityTypeId });
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserActivities)
                .HasForeignKey(d => d.UsrSid);
        }
    }
}
