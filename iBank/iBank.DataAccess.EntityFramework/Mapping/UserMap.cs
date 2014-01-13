using System.Data.Entity.ModelConfiguration;
using iBank.DataAccess.Entities;

namespace iBank.DataAccess.EntityFramework.Mapping
{
    public class UserMap:EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            //this.Property(t => t.Description)
            //    .HasMaxLength(65535);

            // Table & Column Mappings
            this.ToTable("User", "gbua_space2");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}