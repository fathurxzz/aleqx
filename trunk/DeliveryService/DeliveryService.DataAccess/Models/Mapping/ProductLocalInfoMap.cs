using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DeliveryService.DataAccess.Models.Mapping
{
    public class ProductLocalInfoMap : EntityTypeConfiguration<ProductLocalInfo>
    {
        public ProductLocalInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductLocalInfo", "gbua_delivery");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.CityId).HasColumnName("CityId");

            // Relationships
            this.HasRequired(t => t.City)
                .WithMany(t => t.ProductLocalInfoes)
                .HasForeignKey(d => d.CityId);
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductLocalInfoes)
                .HasForeignKey(d => d.ProductId);

        }
    }
}
