using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class ModuleMap : EntityTypeConfiguration<Module>
    {
        public ModuleMap()
        {
            this.HasKey(t => t.ModuleId);

            this.ToTable("sec_module");

            this.Property(t => t.ModuleId).HasColumnName("module_id");
            this.Property(t => t.Description).HasColumnName("module_description");
        }
    }
}
