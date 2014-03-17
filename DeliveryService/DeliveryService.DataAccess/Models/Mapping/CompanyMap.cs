using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DeliveryService.DataAccess.Models.Mapping
{
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
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

            this.Property(t => t.Description)
                .HasMaxLength(1073741823);

            this.Property(t => t.ImageSource)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Company", "gbua_delivery");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");

            // Relationships
            this.HasMany(t => t.MasterCategories)
                .WithMany(t => t.Companies)
                .Map(m =>
                    {
                        m.ToTable("MasterCategoryCompany", "gbua_delivery");
                        m.MapLeftKey("Companies_Id");
                        m.MapRightKey("MasterCategories_Id");
                    });

            this.HasMany(t => t.Qualities)
                .WithMany(t => t.Companies)
                .Map(m =>
                    {
                        m.ToTable("QualityCompany", "gbua_delivery");
                        m.MapLeftKey("Companies_Id");
                        m.MapRightKey("Qualities_Id");
                    });


        }
    }
}
