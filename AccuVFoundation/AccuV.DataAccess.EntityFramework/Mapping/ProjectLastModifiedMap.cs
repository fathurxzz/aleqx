using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class ProjectLastModifiedMap : EntityTypeConfiguration<ProjectLastModified>
    {

        public ProjectLastModifiedMap()
        {

            // Primary Key
            this.HasKey(t => new { t.ProjectId, t.LastModified, t.DumpCreated});

            // Properties
            this.Property(t => t.ProjectId)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("project_last_modified");
            this.Property(t => t.ProjectId).HasColumnName("Project ID");
            this.Property(t => t.LastModified).HasColumnName("last_modified");
            this.Property(t => t.DumpCreated).HasColumnName("Dump_Created");
        }
    }
}