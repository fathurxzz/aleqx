using System.Data.Entity.ModelConfiguration;
using CashMachine.DataAccess.Entities;

namespace CashMachine.DataAccess.EntityFramework.Mapping
{
    public class CardMap : EntityTypeConfiguration<Card>
    {
        public CardMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Number)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.Pin)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("Card", "gbua_card");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.Locked).HasColumnName("Locked");
            this.Property(t => t.Balance).HasColumnName("Balance");
            this.Property(t => t.Pin).HasColumnName("Pin");
            this.Property(t => t.PinAttemptsCount).HasColumnName("PinAttemptsCount");
        }
    }
}
