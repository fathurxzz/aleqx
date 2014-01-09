using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexiusFusion.DataAccess.Entities.Mapping
{
    public class PerDiemMap : EntityTypeConfiguration<PerDiem>
    {
        public PerDiemMap()
        {
            this.HasKey(t => t.PerDiemId);

            this.ToTable("dfr_per_diem");
            this.Property(t => t.Active).HasColumnName("active");
            this.Property(t => t.Value).HasColumnName("per_diem_value");
        }
    }
}
