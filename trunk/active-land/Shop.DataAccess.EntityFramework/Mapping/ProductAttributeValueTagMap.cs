using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ProductAttributeValueTagMap : EntityTypeConfiguration<ProductAttributeValueTag>
    {
        public ProductAttributeValueTagMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductAttributeValueTag", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
        }
    }
}
