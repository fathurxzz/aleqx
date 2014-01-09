using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class MarketMap : EntityTypeConfiguration<Market>
    {
        public MarketMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MarketId, t.Project_ID });

            // Properties
            this.Property(t => t.MarketId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Region)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ManagerId)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Project_ID)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Market");
            this.Property(t => t.MarketId).HasColumnName("Market ID");
            this.Property(t => t.Region).HasColumnName("Region");
            this.Property(t => t.ManagerId).HasColumnName("Manager ID");
            this.Property(t => t.Project_ID).HasColumnName("Project ID");

            // Relationships
            this.HasRequired(t => t.Manager)
                .WithMany(t => t.Markets)
                .HasForeignKey(d => d.ManagerId);
            this.HasRequired(t => t.Project)
                .WithMany(t => t.Markets)
                .HasForeignKey(d => d.Project_ID);

        }
    }
}
