using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class UploadedDocumentMap : EntityTypeConfiguration<UploadedDocument>
    {
        public UploadedDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.DocUplId);

            // Properties
            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.FilePath)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Prefix)
                .HasMaxLength(50);

            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.FileId)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Documents_Uploaded");
            this.Property(t => t.DocUplId).HasColumnName("doc_upl_id");
            this.Property(t => t.DocReqId).HasColumnName("doc_req_id");
            this.Property(t => t.FileName).HasColumnName("file_name");
            this.Property(t => t.FilePath).HasColumnName("file_path");
            this.Property(t => t.Prefix).HasColumnName("prefix");
            //this.Property(t => t.Deleted).HasColumnName("deleted");
            this.Property(t => t.UserId).HasColumnName("user_id");
            this.Property(t => t.LastModified).HasColumnName("last_modified");
            this.Property(t => t.FileId).HasColumnName("file_id");
            this.Property(t => t.Status).HasColumnName("status");
            this.Property(t => t.Active).HasColumnName("active");
            this.Property(t => t.Comment).HasColumnName("comment");

            // Relationships
            this.HasRequired(t => t.RequiredDocuments)
                .WithMany(t => t.UploadedDocuments)
                .HasForeignKey(d => d.DocReqId);

        }
    }
}
