using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.EntityFramework.Mapping
{
    public class AttentionMap : EntityTypeConfiguration<Attention>
    {
        public AttentionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Text)
                .HasMaxLength(200);

            this.Property(t => t.ImageSource)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Attention", "gbua_kiki");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
        }
    }
}
