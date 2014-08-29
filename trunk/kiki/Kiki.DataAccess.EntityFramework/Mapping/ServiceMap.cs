using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.EntityFramework.Mapping
{
    public class ServiceMap : EntityTypeConfiguration<Service>
    {
        public ServiceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Title)
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(1000);

            this.Property(t => t.Price)
                .HasMaxLength(50);

            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(10000);

            this.Property(t => t.ImageSource)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Service", "gbua_kiki");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
        }
    }
}
