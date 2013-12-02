using System;
using System.Collections.Generic;

namespace SpaceGame.Data2.Models
{
    public partial class PlanetFacility
    {
        public int Id { get; set; }
        public short Level { get; set; }
        public System.DateTime UpdateStart { get; set; }
        public bool IsUpdating { get; set; }
        public int PlanetId { get; set; }
        public int FacilityId { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual Planet Planet { get; set; }
    }
}
