using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.EntityFramework.Mapping
{
    public class ServiceItemMap : EntityTypeConfiguration<ServiceItem>
    {
        public ServiceItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(1000);

            this.Property(t => t.Price)
                .HasMaxLength(50);

            this.Property(t => t.Text)
                .HasMaxLength(10000);

            // Table & Column Mappings
            this.ToTable("ServiceItem", "gbua_kiki");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.ServiceId).HasColumnName("ServiceId");

            // Relationships
            this.HasRequired(t => t.Service)
                .WithMany(t => t.ServiceItems)
                .HasForeignKey(d => d.ServiceId);

        }
    }
}
