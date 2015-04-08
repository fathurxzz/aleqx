using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class SitePropertyMap : EntityTypeConfiguration<SiteProperty>
    {
        public SitePropertyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.Name)
                .HasMaxLength(500);

            this.Property(t => t.Title)
                .HasMaxLength(500);

            this.Property(t => t.Value)
                .HasMaxLength(1000);

           

            this.ToTable("SiteProperty", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Value).HasColumnName("Value");
        }
    }
}
