using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserSid);

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Company)
                .HasMaxLength(100);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("sec_user");
            this.Property(t => t.UserSid).HasColumnName("usr_sid");
            this.Property(t => t.Id).HasColumnName("usr_id");
            this.Property(t => t.UserName).HasColumnName("usr_desc");
            this.Property(t => t.Active).HasColumnName("active");
            this.Property(t => t.Company).HasColumnName("user_comp");
            this.Property(t => t.Email).HasColumnName("user_email");

        }
    }
}
