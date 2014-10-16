using System.Data.Entity.ModelConfiguration;


namespace Leo.Models.Mapping
{
    public class ProductTextBlockFileMap : EntityTypeConfiguration<ProductTextBlockFile>
    {
        public ProductTextBlockFileMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSource)
                .HasMaxLength(500);

            // Properties
            this.Property(t => t.FileName)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ProductTextBlockFile", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.ProductTextBlockId).HasColumnName("ProductTextBlockId");

            // Relationships
            this.HasRequired(t => t.ProductTextBlock)
                .WithMany(t => t.ProductTextBlockFiles)
                .HasForeignKey(d => d.ProductTextBlockId);
        }
    }
}