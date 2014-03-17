using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DeliveryService.DataAccess.Models.Mapping
{
    public class CompanyLocalInfoMap : EntityTypeConfiguration<CompanyLocalInfo>
    {
        public CompanyLocalInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.WorkTime)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            this.Property(t => t.MinOrderAmount)
                .IsRequired()
                .HasMaxLength(1073741823);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CompanyLocalInfo", "gbua_delivery");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.WorkTime).HasColumnName("WorkTime");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
            this.Property(t => t.CityId).HasColumnName("CityId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DeliveryCost).HasColumnName("DeliveryCost");
            this.Property(t => t.MinOrderAmount).HasColumnName("MinOrderAmount");
            this.Property(t => t.Title).HasColumnName("Title");

            // Relationships
            this.HasMany(t => t.Criteria)
                .WithMany(t => t.CompanyLocalInfoes)
                .Map(m =>
                    {
                        m.ToTable("CriteriaCompanyLocalInfo", "gbua_delivery");
                        m.MapLeftKey("CompanyLocalInfo_Id");
                        m.MapRightKey("Criteria_Id");
                    });

            this.HasRequired(t => t.City)
                .WithMany(t => t.CompanyLocalInfoes)
                .HasForeignKey(d => d.CityId);
            this.HasRequired(t => t.Company)
                .WithMany(t => t.CompanyLocalInfoes)
                .HasForeignKey(d => d.CompanyId);

        }
    }
}
