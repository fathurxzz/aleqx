using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.EntityFramework.Mapping
{
    public class SaleMap : EntityTypeConfiguration<Sale>
    {
        public SaleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(1000);

            this.Property(t => t.Text)
                .HasMaxLength(10000);

            this.Property(t => t.ImageSource)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Sale", "gbua_kiki");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
        }
    }
}
