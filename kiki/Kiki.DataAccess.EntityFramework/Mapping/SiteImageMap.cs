using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.EntityFramework.Mapping
{
    public class SiteImageMap : EntityTypeConfiguration<SiteImage>
    {
        public SiteImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSource)
                .IsRequired()
                .HasMaxLength(200);
            
            this.Property(t => t.Text)
                .HasMaxLength(200);

            this.Property(t => t.TextEng)
                .HasMaxLength(200);

            this.Property(t => t.Text2)
                .HasMaxLength(200);

            this.Property(t => t.Text2Eng)
                .HasMaxLength(200);

            this.Property(t => t.Text0)
                .HasMaxLength(200);

            this.Property(t => t.Text0Eng)
                .HasMaxLength(200);




            // Table & Column Mappings
            this.ToTable("SiteImage", "gbua_kiki");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.TextEng).HasColumnName("TextEng");
            this.Property(t => t.Text2).HasColumnName("Text2");
            this.Property(t => t.Text2Eng).HasColumnName("Text2Eng");
            this.Property(t => t.Text0).HasColumnName("Text0");
            this.Property(t => t.Text0Eng).HasColumnName("Text0Eng");
            this.Property(t => t.ImageType).HasColumnName("ImageType");
        }
    }
}
