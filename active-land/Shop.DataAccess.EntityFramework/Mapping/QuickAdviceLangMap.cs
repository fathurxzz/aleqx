using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class QuickAdviceLangMap : EntityTypeConfiguration<QuickAdviceLang>
    {
        public QuickAdviceLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(10000);

            // Table & Column Mappings
            this.ToTable("QuickAdviceLang", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.QuickAdviceId).HasColumnName("QuickAdviceId");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Text).HasColumnName("Text");

            // Relationships
            this.HasRequired(t => t.Language)
                .WithMany(t => t.QuickAdviceLangs)
                .HasForeignKey(d => d.LanguageId);
            this.HasRequired(t => t.QuickAdvice)
                .WithMany(t => t.QuickAdviceLangs)
                .HasForeignKey(d => d.QuickAdviceId);

        }
    }
}
