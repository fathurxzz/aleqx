using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using AccuV.DataAccess.Entities;

namespace AccuV.DataAccess.EntityFramework.Mapping
{
    public class SecurityViewMap:EntityTypeConfiguration<SecurityView>
    {
        public SecurityViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProjectId, t.ModuleId,t.UsrId,t.ActivityId,t.SiteNumber,t.TaskId });

            // Table & Column Mappings
            this.ToTable("security_view");
            this.Property(t => t.ProjectId).HasColumnName("project_id");
            this.Property(t => t.ModuleId).HasColumnName("module_id");
            this.Property(t => t.ModuleDescription).HasColumnName("module_description");
            this.Property(t => t.UsrId).HasColumnName("usr_id");
            this.Property(t => t.UsrDesc).HasColumnName("usr_desc");
            this.Property(t => t.TypeActivityId).HasColumnName("type_activity_id");
            this.Property(t => t.TypeActivity).HasColumnName("type_activity");
            this.Property(t => t.ActivityId).HasColumnName("activity_id");
            this.Property(t => t.ActivityDescription).HasColumnName("activity_description");
            this.Property(t => t.SiteNumber).HasColumnName("site number");
            this.Property(t => t.TaskId).HasColumnName("task id");
            this.Property(t => t.AccessCreate).HasColumnName("access_create");
            this.Property(t => t.AccessRead).HasColumnName("access_read");
            this.Property(t => t.AccessUpdate).HasColumnName("access_update");
            this.Property(t => t.AccessDelete).HasColumnName("access_delete");
            this.Property(t => t.AccessApprove).HasColumnName("access_approve");
            this.Property(t => t.AccessAdmin).HasColumnName("access_admin");
        }

        
    }
}
