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




            // Table & Column Mappings
            this.ToTable("SiteImage", "gbua_kiki");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.ImageType).HasColumnName("ImageType");
        }
    }
}
