using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectId);

            // Properties
            this.Property(t => t.ProjectId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ProjectName)
                .IsFixedLength()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Project");
            this.Property(t => t.ProjectId).HasColumnName("Project ID");
            this.Property(t => t.ProjectName).HasColumnName("Project Name");
        }
    }
}
