using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
{
    public class QuickAdviceMap : EntityTypeConfiguration<QuickAdvice>
    {
        public QuickAdviceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("QuickAdvice", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
        }
    }
}
