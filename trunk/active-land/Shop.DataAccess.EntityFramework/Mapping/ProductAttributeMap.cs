using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ProductAttributeMap : EntityTypeConfiguration<ProductAttribute>
    {
        public ProductAttributeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductAttribute", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.IsStatic).HasColumnName("IsStatic");
            this.Property(t => t.IsPublic).HasColumnName("IsPublic");
            this.Property(t => t.DisplayOnPreview).HasColumnName("DisplayOnPreview");
            this.Property(t => t.IsFilterable).HasColumnName("IsFilterable");

            // Ignored
            this.Ignore(t => t.Title);
            this.Ignore(t => t.UnitTitle);
            this.Ignore(t => t.IsCorrectLang);
            this.Ignore(t => t.CurrentLang);
            this.Ignore(t => t.Selected);

        }
    }
}
