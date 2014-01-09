using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NexiusFusion.DataAccess.Entities.Mapping
{
    public class EmployeeTypeMap : EntityTypeConfiguration<EmployeeType>
    {
        public EmployeeTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.EmployeeTypeId);

            // Properties
            this.Property(t => t.EmployeeTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EmployeeTypeDescrition)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("dfr_employee_type");
            this.Property(t => t.EmployeeTypeId).HasColumnName("employee_type_id");
            this.Property(t => t.EmployeeTypeDescrition).HasColumnName("employee_type_descrition");
        }
    }
}
