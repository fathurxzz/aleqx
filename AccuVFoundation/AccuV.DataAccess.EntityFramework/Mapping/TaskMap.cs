using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TaskId, t.ProjectId });

            // Properties
            this.Property(t => t.TaskId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ProjectId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.TaskName)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Tasks");
            this.Property(t => t.TaskId).HasColumnName("Task ID");
            this.Property(t => t.ProjectId).HasColumnName("Project ID");
            this.Property(t => t.TaskName).HasColumnName("Task Name");

            // Relationships
            this.HasMany(t => t.Activities)
                .WithMany(t => t.Tasks)
                .Map(m =>
                {
                    m.ToTable("sec_task_activity");
                    m.MapLeftKey("task_id", "project_id");
                    m.MapRightKey("activity", "type_activity");
                });

            this.HasMany(t => t.ActivityTypes)
                .WithMany(t => t.Tasks)
                .Map(m =>
                {
                    m.ToTable("sec_type_task");
                    m.MapLeftKey("task_id", "project_id");
                    m.MapRightKey("type_task");
                });

            this.HasRequired(t => t.Project)
                .WithMany(t => t.Tasks)
                .HasForeignKey(d => d.ProjectId);

        }
    }
}
