using System.Collections;
using System.Collections.Generic;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.UI.Models
{
    public class FacilityViewModel:GameViewModel
    {
        public IEnumerable<PlanetFacility> PlanetFacilities { get; set; }
    }
}