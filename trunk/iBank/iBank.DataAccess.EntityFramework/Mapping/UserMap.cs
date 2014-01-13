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
            this.Property(t => t.Login)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Password)
               .IsRequired()
               .HasMaxLength(50);

            //this.Property(t => t.Description)
            //    .HasMaxLength(65535);

            // Table & Column Mappings
            this.ToTable("User", "iBankDev");
            
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Login).HasColumnName("Login");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.PasswordExpireDate).HasColumnName("PasswordExpireDate");

        }
    }
}