using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class TaskCommentMap : EntityTypeConfiguration<TaskComment>
    {
        public TaskCommentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TaskId, t.SiteNumber, t.CommentDate });

            // Properties

            /*this.Property(t => t.ID)
                .IsRequired();*/

            this.Property(t => t.TaskId)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.SiteNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProjectId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CommentOn)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Comment)
                .HasMaxLength(500);

            this.Property(t => t.CommentDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Comments");
            //this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TaskId).HasColumnName("Task ID");
            this.Property(t => t.SiteNumber).HasColumnName("Site Number");
            this.Property(t => t.ProjectId).HasColumnName("Project ID");
            this.Property(t => t.UserId).HasColumnName("User ID");
            this.Property(t => t.CompletedDate).HasColumnName("Completed Date");
            this.Property(t => t.ForecastDate).HasColumnName("Forecast Date");
            this.Property(t => t.CommentOn).HasColumnName("Comment On");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.CommentDate).HasColumnName("Comment Date");
        }
    }
}
