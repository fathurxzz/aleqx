using System.Data.Entity.ModelConfiguration;

namespace NexiusFusion.DataAccess.Entities.Mapping
{
    public class WorkTypeMap : EntityTypeConfiguration<WorkType>
    {
        public WorkTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.WorkTypeId);

            // Properties
            this.Property(t => t.WorkTypeDescription)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("dfr_work_type");
            this.Property(t => t.WorkTypeId).HasColumnName("work_type_id");
            this.Property(t => t.WorkTypeDescription).HasColumnName("work_type_description");
        }
    }
}
