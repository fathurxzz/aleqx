using System.Data.Entity.ModelConfiguration;

namespace NexiusFusion.DataAccess.Entities.Mapping
{
    public class WeatherMap : EntityTypeConfiguration<Weather>
    {
        public WeatherMap()
        {
            // Primary Key
            this.HasKey(t => t.WeatherId);

            // Properties
            this.Property(t => t.WeatherDescription)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("dfr_weather");
            this.Property(t => t.WeatherId).HasColumnName("weather_id");
            this.Property(t => t.WeatherDescription).HasColumnName("weather_description");
        }
    }
}
