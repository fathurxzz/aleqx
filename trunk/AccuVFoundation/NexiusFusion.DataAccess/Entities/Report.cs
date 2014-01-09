using System;
using System.Collections.Generic;

namespace NexiusFusion.DataAccess.Entities
{
    public partial class Report
    {
        public Report()
        {
            this.ReportEmployees = new List<ReportEmployee>();
            this.ReportHazards = new List<ReportHazard>();
        }

        public int ReportId { get; set; }
        public string SiteNumber { get; set; }
        public int FaLocation { get; set; }
        public string ProjectId { get; set; }
        public string PhysicalAddress { get; set; }
        public System.DateTime ReportDate { get; set; }
        public Nullable<System.TimeSpan> DriveSiteTime { get; set; }
        public Nullable<System.TimeSpan> ArrivalSiteTime { get; set; }
        public Nullable<System.TimeSpan> CrewLeftSiteTime { get; set; }
        public Nullable<System.TimeSpan> ArrivalHotelNextSiteTime { get; set; }
        public int WorkTypeId { get; set; }
        public int WeatherId { get; set; }
        public Nullable<bool> ToolBoxTalks { get; set; }
        public Nullable<bool> Trenching { get; set; }
        public Nullable<bool> JsaCompleted { get; set; }
        public Nullable<bool> UtilityCompNotified { get; set; }
        public Nullable<bool> SafetyEquipmentInspection { get; set; }
        public Nullable<bool> SafetySignsOnSite { get; set; }
        public Nullable<System.TimeSpan> CrewForemanPaperworkStartTime { get; set; }
        public Nullable<System.TimeSpan> CrewForemanPaperworkStopTime { get; set; }
        public int StructureTypeId { get; set; }
        public string WorkPerformedCompleted { get; set; }
        public string WorkNotCompleted { get; set; }
        public string CommetsWaWork { get; set; }
        public string AdditionalMaterial { get; set; }
        public Nullable<bool> Submitted { get; set; }
        public Nullable<System.TimeSpan> AlphaTimeDown { get; set; }
        public Nullable<System.TimeSpan> AlphaTimeUp { get; set; }
        public Nullable<System.TimeSpan> BetaTimeDown { get; set; }
        public Nullable<System.TimeSpan> BetaTimeUp { get; set; }
        public Nullable<System.TimeSpan> GammaTimeDown { get; set; }
        public Nullable<System.TimeSpan> GammaTimeUp { get; set; }
        public Nullable<System.TimeSpan> DeltaTimeDown { get; set; }
        public Nullable<System.TimeSpan> DeltaTimeUp { get; set; }
        public virtual ICollection<ReportEmployee> ReportEmployees { get; set; }
        public virtual ICollection<ReportHazard> ReportHazards { get; set; }
        public virtual Site Site { get; set; }
        public virtual StructureType StructureType { get; set; }
        public virtual Weather Weather { get; set; }
        public virtual WorkType WorkType { get; set; }
    }
}
