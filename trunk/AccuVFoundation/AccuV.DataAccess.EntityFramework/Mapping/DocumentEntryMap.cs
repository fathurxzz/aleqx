using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class DocumentEntryMap : EntityTypeConfiguration<DocumentEntry>
    {
        public DocumentEntryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DocId, t.Category, t.Type, t.ProjectId });

            // Properties
            this.Property(t => t.DocId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.DocName)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Category)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProjectId)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Documents_List");
            this.Property(t => t.DocId).HasColumnName("doc_id");
            this.Property(t => t.DocName).HasColumnName("doc_name");
            this.Property(t => t.Category).HasColumnName("category");
            this.Property(t => t.Type).HasColumnName("type");
            this.Property(t => t.ProjectId).HasColumnName("project_id");
        }
    }
}
