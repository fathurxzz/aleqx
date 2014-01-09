using System.Collections.Generic;

namespace NexiusFusion.DataAccess.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            this.ReportEmployees = new List<ReportEmployee>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<ReportEmployee> ReportEmployees { get; set; }
    }
}
