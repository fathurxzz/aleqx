using System;
using System.Collections.Generic;

namespace SpaceGame.Data2.Models
{
    public partial class Planet
    {
        public Planet()
        {
            this.PlanetFacilities = new List<PlanetFacility>();
            this.PlanetResources = new List<PlanetResource>();
            this.PlanetShips = new List<PlanetShip>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<PlanetFacility> PlanetFacilities { get; set; }
        public virtual ICollection<PlanetResource> PlanetResources { get; set; }
        public virtual ICollection<PlanetShip> PlanetShips { get; set; }
        public virtual User User { get; set; }
    }
}
