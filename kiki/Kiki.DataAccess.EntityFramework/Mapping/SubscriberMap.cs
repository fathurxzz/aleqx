using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.EntityFramework.Mapping
{
    public class SubscriberMap : EntityTypeConfiguration<Subscriber>
    {
        public SubscriberMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Subscriber", "gbua_kiki");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Email).HasColumnName("Email");
        }
    }
}
