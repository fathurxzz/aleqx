using System.Collections.Generic;

namespace NexiusFusion.DataAccess.Entities
{
    public partial class StructureType
    {
        public StructureType()
        {
            this.Reports = new List<Report>();
        }

        public int StructureTypeId { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
