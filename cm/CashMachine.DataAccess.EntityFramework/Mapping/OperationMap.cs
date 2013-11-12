using System.Data.Entity.ModelConfiguration;
using CashMachine.DataAccess.Entities;

namespace CashMachine.DataAccess.EntityFramework.Mapping
{
    public class OperationMap : EntityTypeConfiguration<Operation>
    {
        public OperationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Operation", "gbua_card");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.CardId).HasColumnName("CardId");
            this.Property(t => t.OperationType).HasColumnName("OperationTypeId");

            // Relationships
            this.HasRequired(t => t.Card)
                .WithMany(t => t.Operations)
                .HasForeignKey(d => d.CardId);

        }
    }
}
