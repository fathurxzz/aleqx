using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
{
    public class OrderItemMap : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(1000);

            this.Property(t => t.ImageSource)
                .HasMaxLength(200);

            this.Property(t => t.ProductName)
                .HasMaxLength(200);

            this.Property(t => t.ProductTitle)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("OrderItem", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.ProductTitle).HasColumnName("ProductTitle");
            this.Property(t => t.OrderId).HasColumnName("OrderId");

            // Relationships
            this.HasRequired(t => t.Order)
                .WithMany(t => t.OrderItems)
                .HasForeignKey(d => d.OrderId);

        }
    }
}
