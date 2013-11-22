using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TheSmallSpaceGame.DataAccess.Entities;

namespace TheSmallSpaceGame.DataAccess.EntityFramework.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("User", "gbua_sgame");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            //this.Property(t => t.Resources_Id).HasColumnName("Resources_Id");

            // Relationships
            //this.HasRequired(t => t.Resource)
            //    .WithMany(t => t.Users)
            //    .HasForeignKey(d => d.Resources_Id);

        }
    }
}
