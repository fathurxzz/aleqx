using System.Data.Entity.ModelConfiguration;

namespace NexiusFusion.DataAccess.Entities.Mapping
{
    public class SiteMap : EntityTypeConfiguration<Site>
    {
        public SiteMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SiteNumber, t.ProjectId });

            // Properties
            this.Property(t => t.SiteNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProjectId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.SiteName)
                .HasMaxLength(250);

            this.Property(t => t.SiteDesign)
                .HasMaxLength(100);

            this.Property(t => t.SiteType)
                .HasMaxLength(100);

            this.Property(t => t.MarketId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ManagerId)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Site");
            this.Property(t => t.SiteNumber).HasColumnName("Site Number");
            this.Property(t => t.ProjectId).HasColumnName("Project ID");
            this.Property(t => t.SiteName).HasColumnName("Site Name");
            this.Property(t => t.SiteDesign).HasColumnName("Site Design");
            this.Property(t => t.SiteType).HasColumnName("Site Type");
            this.Property(t => t.MarketId).HasColumnName("Market ID");
            this.Property(t => t.ManagerId).HasColumnName("Manager ID");

            // Relationships

            this.HasRequired(t => t.Project)
                .WithMany(t => t.Sites)
                .HasForeignKey(d => d.ProjectId);

        }
    }
}
