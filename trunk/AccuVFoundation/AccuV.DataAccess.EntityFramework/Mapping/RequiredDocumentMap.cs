using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class RequiredDocumentMap : EntityTypeConfiguration<RequiredDocument>
    {
        public RequiredDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.DocReqId);

            // Properties
            this.Property(t => t.DocId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ProjectId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.SiteNumber)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.TaskId)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Documents_Needed");
            this.Property(t => t.DocReqId).HasColumnName("doc_req_id");
            this.Property(t => t.DocId).HasColumnName("doc_id");
            this.Property(t => t.ProjectId).HasColumnName("project_id");
            this.Property(t => t.SiteNumber).HasColumnName("site_number");
            this.Property(t => t.TaskId).HasColumnName("task_id");
            this.Property(t => t.Active).HasColumnName("active");
            this.Property(t => t.LastModified).HasColumnName("last_modified");
        }
    }
}
