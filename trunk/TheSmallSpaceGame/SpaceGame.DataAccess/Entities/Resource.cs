using System;

namespace SpaceGame.DataAccess.Entities
{
    public partial class Resource
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime LastUpdate { get; set; }
        public short MineLevel { get; set; }
        public int PlanetId { get; set; }
        public int ResourceTypeId { get; set; }
        public virtual Planet Planet { get; set; }
        public virtual ResourceType ResourceType { get; set; }
    }
}
