using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ProductStockMap : EntityTypeConfiguration<ProductStock>
    {
        public ProductStockMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.StockNumber)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ProductStock", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.StockNumber).HasColumnName("StockNumber");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.Color).HasColumnName("Color");
            this.Property(t => t.IsAvailable).HasColumnName("IsAvailable");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductStocks)
                .HasForeignKey(d => d.ProductId);
        }
    }
}
