using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class ManagerMap : EntityTypeConfiguration<Manager>
    {
        public ManagerMap()
        {
            // Primary Key
            this.HasKey(t => t.ManagerId);

            // Properties
            this.Property(t => t.ManagerId)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ManagerName)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Manager");
            this.Property(t => t.ManagerId).HasColumnName("Manager ID");
            this.Property(t => t.ManagerName).HasColumnName("Manager Name");
        }
    }
}
