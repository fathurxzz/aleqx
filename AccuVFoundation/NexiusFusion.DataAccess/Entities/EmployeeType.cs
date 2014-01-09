using System.Collections.Generic;

namespace NexiusFusion.DataAccess.Entities
{
    public partial class EmployeeType
    {
        public EmployeeType()
        {
            this.ReportEmployees = new List<ReportEmployee>();
        }

        public int EmployeeTypeId { get; set; }
        public string EmployeeTypeDescrition { get; set; }
        public virtual ICollection<ReportEmployee> ReportEmployees { get; set; }
    }
}
