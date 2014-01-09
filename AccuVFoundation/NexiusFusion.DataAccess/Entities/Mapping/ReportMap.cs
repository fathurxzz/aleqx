using System.Data.Entity.ModelConfiguration;

namespace NexiusFusion.DataAccess.Entities.Mapping
{
    public class ReportMap : EntityTypeConfiguration<Report>
    {
        public ReportMap()
        {
            // Primary Key
            this.HasKey(t => t.ReportId);

            // Properties
            this.Property(t => t.SiteNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProjectId)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("dfr_nexius_fusion");
            this.Property(t => t.ReportId).HasColumnName("dfr_id");
            this.Property(t => t.SiteNumber).HasColumnName("site_number");
            this.Property(t => t.FaLocation).HasColumnName("fa_location");
            this.Property(t => t.ProjectId).HasColumnName("project_id");
            this.Property(t => t.PhysicalAddress).HasColumnName("physical_address");
            this.Property(t => t.ReportDate).HasColumnName("dfr_date");
            this.Property(t => t.DriveSiteTime).HasColumnName("drive_site_time");
            this.Property(t => t.ArrivalSiteTime).HasColumnName("arrival_site_time");
            this.Property(t => t.CrewLeftSiteTime).HasColumnName("crew_left_site_time");
            this.Property(t => t.ArrivalHotelNextSiteTime).HasColumnName("arrival_hotel_next_site_time");
            this.Property(t => t.WorkTypeId).HasColumnName("work_type_id");
            this.Property(t => t.WeatherId).HasColumnName("weather_id");
            this.Property(t => t.ToolBoxTalks).HasColumnName("tool_box_talks");
            this.Property(t => t.Trenching).HasColumnName("trenching");
            this.Property(t => t.JsaCompleted).HasColumnName("jsa_completed");
            this.Property(t => t.UtilityCompNotified).HasColumnName("utility_comp_notified");
            this.Property(t => t.SafetyEquipmentInspection).HasColumnName("safety_equipment_inspection");
            this.Property(t => t.SafetySignsOnSite).HasColumnName("safety_signs_on_site");
            this.Property(t => t.CrewForemanPaperworkStartTime).HasColumnName("crew_foreman_paperwork_start_time");
            this.Property(t => t.CrewForemanPaperworkStopTime).HasColumnName("crew_foreman_paperwork_stop_time");
            this.Property(t => t.StructureTypeId).HasColumnName("structure_type_id");
            this.Property(t => t.WorkPerformedCompleted).HasColumnName("work_performaed_completed");
            this.Property(t => t.WorkNotCompleted).HasColumnName("work_not_completed");
            this.Property(t => t.CommetsWaWork).HasColumnName("commets_wa_work");
            this.Property(t => t.AdditionalMaterial).HasColumnName("additional_material");
            this.Property(t => t.Submitted).HasColumnName("submitted");
            this.Property(t => t.AlphaTimeDown).HasColumnName("alfa_time_down");
            this.Property(t => t.AlphaTimeUp).HasColumnName("alfa_time_up");
            this.Property(t => t.BetaTimeDown).HasColumnName("beta_time_down");
            this.Property(t => t.BetaTimeUp).HasColumnName("beta_time_up");
            this.Property(t => t.GammaTimeDown).HasColumnName("gamma_time_down");
            this.Property(t => t.GammaTimeUp).HasColumnName("gamma_time_up");
            this.Property(t => t.DeltaTimeDown).HasColumnName("delta_time_down");
            this.Property(t => t.DeltaTimeUp).HasColumnName("delta_time_up");

            // Relationships
            this.HasRequired(t => t.Site)
                .WithMany(t => t.Reports)
                .HasForeignKey(d => new { d.SiteNumber, d.ProjectId });
            this.HasRequired(t => t.StructureType)
                .WithMany(t => t.Reports)
                .HasForeignKey(d => d.StructureTypeId);
            this.HasRequired(t => t.Weather)
                .WithMany(t => t.Reports)
                .HasForeignKey(d => d.WeatherId);
            this.HasRequired(t => t.WorkType)
                .WithMany(t => t.Reports)
                .HasForeignKey(d => d.WorkTypeId);

        }
    }
}
