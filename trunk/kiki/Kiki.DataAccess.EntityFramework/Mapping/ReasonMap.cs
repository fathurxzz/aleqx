using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.EntityFramework.Mapping
{
    public class ReasonMap : EntityTypeConfiguration<Reason>
    {
        public ReasonMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.TitleEng)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Text)
                .HasMaxLength(1000);

            this.Property(t => t.TextEng)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("Reason", "gbua_kiki");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.TitleEng).HasColumnName("TitleEng");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.TextEng).HasColumnName("TextEng");
        }
    }
}
