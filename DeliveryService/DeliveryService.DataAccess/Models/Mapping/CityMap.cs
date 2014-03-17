using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DeliveryService.DataAccess.Models.Mapping
{
    public class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("City", "gbua_delivery");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasMany(t => t.Companies)
                .WithMany(t => t.Cities)
                .Map(m =>
                    {
                        m.ToTable("CityCompany", "gbua_delivery");
                        m.MapLeftKey("Cities_Id");
                        m.MapRightKey("Companies_Id");
                    });

            this.HasMany(t => t.MasterCategories)
                .WithMany(t => t.Cities)
                .Map(m =>
                    {
                        m.ToTable("CityMasterCategory", "gbua_delivery");
                        m.MapLeftKey("Cities_Id");
                        m.MapRightKey("MasterCategories_Id");
                    });


        }
    }
}
