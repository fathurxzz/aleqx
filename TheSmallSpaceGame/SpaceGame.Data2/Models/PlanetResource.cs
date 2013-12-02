using System;
using System.Collections.Generic;

namespace SpaceGame.Data2.Models
{
    public partial class PlanetResource
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public short MineLevel { get; set; }
        public int ResourceId { get; set; }
        public int PlanetId { get; set; }
        public virtual Planet Planet { get; set; }
        public virtual Resource Resource { get; set; }
    }
}
