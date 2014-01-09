using System.Data.Entity.ModelConfiguration;

namespace NexiusFusion.DataAccess.Entities.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            // Primary Key
            this.HasKey(t => t.EmployeeId);

            // Properties
            this.Property(t => t.EmployeeName)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("dfr_employee");
            this.Property(t => t.EmployeeId).HasColumnName("employee_id");
            this.Property(t => t.EmployeeName).HasColumnName("employee_name");
            this.Property(t => t.Active).HasColumnName("active");
        }
    }
}
