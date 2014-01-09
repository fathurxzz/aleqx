using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NexiusFusion.DataAccess.Entities.Mapping
{
    public class ReportEmployeeMap : EntityTypeConfiguration<ReportEmployee>
    {
        public ReportEmployeeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ReportId, t.EmployeeId });

            // Properties
            this.Property(t => t.ReportId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EmployeeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("dfr_dfr_employee");
            this.Property(t => t.ReportId).HasColumnName("dfr_id");
            this.Property(t => t.EmployeeId).HasColumnName("employee_id");
            this.Property(t => t.EmployeeTypeId).HasColumnName("employee_type_id");
            this.Property(t => t.StartTime).HasColumnName("start_time");
            this.Property(t => t.LunchStart).HasColumnName("lunch_start");
            this.Property(t => t.LunchEnd).HasColumnName("lunch_end");
            this.Property(t => t.StopTime).HasColumnName("stop_time");
            this.Property(t => t.PerDiemId).HasColumnName("per_diem_id");

            // Relationships
            this.HasRequired(t => t.Report)
                .WithMany(t => t.ReportEmployees)
                .HasForeignKey(d => d.ReportId);
            this.HasRequired(t => t.Employee)
                .WithMany(t => t.ReportEmployees)
                .HasForeignKey(d => d.EmployeeId);
            this.HasRequired(t => t.EmployeeType)
                .WithMany(t => t.ReportEmployees)
                .HasForeignKey(d => d.EmployeeTypeId);
            this.HasRequired(t => t.PerDiem)
                .WithMany(t => t.ReportEmployees)
                .HasForeignKey(d => d.PerDiemId);
        }
    }
}
