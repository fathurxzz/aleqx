using System.Collections.Generic;

namespace SpaceGame.DataAccess.Entities
{
    public partial class Ship
    {
        public Ship()
        {
            this.PlanetShips = new List<PlanetShip>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public short Health { get; set; }
        public short Offence { get; set; }
        public short Capacity { get; set; }
        public short Deffence { get; set; }
        public virtual ICollection<PlanetShip> PlanetShips { get; set; }
    }
}