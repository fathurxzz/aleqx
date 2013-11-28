using System;
using System.Collections.Generic;

namespace SpaceGame.Data2.Models
{
    public partial class Resource
    {
        public int Id { get; set; }
        public long Amount { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public short MineLevel { get; set; }
        public int PlanetId { get; set; }
        public int ResourceTypeId { get; set; }
        public virtual Planet Planet { get; set; }
        public virtual ResourceType ResourceType { get; set; }
    }
}
