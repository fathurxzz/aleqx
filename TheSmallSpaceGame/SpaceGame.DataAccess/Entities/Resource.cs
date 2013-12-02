using System;
using System.Collections.Generic;

namespace SpaceGame.DataAccess.Entities
{
    public partial class Resource
    {
        public Resource()
        {
            this.PlanetResources = new List<PlanetResource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PlanetResource> PlanetResources { get; set; }
    }
}
