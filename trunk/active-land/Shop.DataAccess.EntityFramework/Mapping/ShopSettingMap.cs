using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ShopSettingMap : EntityTypeConfiguration<ShopSetting>
    {
        public ShopSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Key)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Value)
                .HasMaxLength(200);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ShopSetting", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Title).HasColumnName("Title");
        }
    }
}
