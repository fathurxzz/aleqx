using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TheSmallSpaceGame.DataAccess.Entities;

namespace TheSmallSpaceGame.DataAccess.EntityFramework.Models.Mapping
{
    public class ResourceMap : EntityTypeConfiguration<Resource>
    {
        public ResourceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Resources", "gbua_sgame");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Metal).HasColumnName("Metal");
            this.Property(t => t.Crystal).HasColumnName("Crystal");
            this.Property(t => t.Deiterium).HasColumnName("Deiterium");
            this.Property(t => t.LastRecalculateDate).HasColumnName("LastRecalculateDate");
        }
    }
}
