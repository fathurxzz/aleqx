using System;

namespace AccuV.DataAccess.Entities
{
    public partial class UserActivity
    {
        public int UsrSid { get; set; }
        public int ActivityId { get; set; }
        public int ActivityTypeId { get; set; }
        public bool AccessCreate { get; set; }
        public bool AccessRead { get; set; }
        public bool AccessUpdate { get; set; }
        public bool AccessDelete { get; set; }
        public bool AccessApprove { get; set; }
        public bool AccessAdmin { get; set; }
        public virtual Activity Activity { get; set; }
        public virtual User User { get; set; }
    }
}
