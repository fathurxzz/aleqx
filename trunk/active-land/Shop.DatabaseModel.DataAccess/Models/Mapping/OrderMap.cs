using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CustomerName)
                .HasMaxLength(200);

            this.Property(t => t.CustomerPhone)
                .HasMaxLength(200);

            this.Property(t => t.CustomerEmail)
                .HasMaxLength(200);

            this.Property(t => t.DeliveryAddress)
                .HasMaxLength(200);

            this.Property(t => t.Info)
                .HasMaxLength(1073741823);

            this.Property(t => t.DeliveryCity)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.DeliveryStreet)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.DeliveryOffice)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Order", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.CustomerPhone).HasColumnName("CustomerPhone");
            this.Property(t => t.CustomerEmail).HasColumnName("CustomerEmail");
            this.Property(t => t.DeliveryAddress).HasColumnName("DeliveryAddress");
            this.Property(t => t.Completed).HasColumnName("Completed");
            this.Property(t => t.Info).HasColumnName("Info");
            this.Property(t => t.Subscribed).HasColumnName("Subscribed");
            this.Property(t => t.DeliveryMethod).HasColumnName("DeliveryMethod");
            this.Property(t => t.DeliveryCity).HasColumnName("DeliveryCity");
            this.Property(t => t.DeliveryStreet).HasColumnName("DeliveryStreet");
            this.Property(t => t.DeliveryOffice).HasColumnName("DeliveryOffice");
            this.Property(t => t.PaymentMethod).HasColumnName("PaymentMethod");
        }
    }
}
