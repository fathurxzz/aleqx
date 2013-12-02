using System;
using System.Collections.Generic;

namespace SpaceGame.Data2.Models
{
    public partial class Facility
    {
        public Facility()
        {
            this.PlanetFacilities = new List<PlanetFacility>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double MetalCost { get; set; }
        public double CrystalCost { get; set; }
        public double DeiteriumCost { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PlanetFacility> PlanetFacilities { get; set; }
    }
}
