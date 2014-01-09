using System;
using NexiusFusion.DataAccess.Entities.Mapping;

namespace NexiusFusion.DataAccess.Entities
{
    public partial class ReportEmployee
    {
        public int ReportId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeTypeId { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> LunchStart { get; set; }
        public Nullable<System.TimeSpan> LunchEnd { get; set; }
        public Nullable<System.TimeSpan> StopTime { get; set; }
        public int PerDiemId { get; set; }
        public virtual Report Report { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public virtual PerDiem PerDiem { get; set; }
    }
}
