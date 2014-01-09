using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class ActivityTypeMap : EntityTypeConfiguration<ActivityType>
    {
        public ActivityTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ActivityTypeId);

            // Properties
            this.Property(t => t.ActivityTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("sec_type_activity");
            this.Property(t => t.ActivityTypeId).HasColumnName("type_activity_id");
            this.Property(t => t.Type).HasColumnName("type_activity");
        }
    }
}
