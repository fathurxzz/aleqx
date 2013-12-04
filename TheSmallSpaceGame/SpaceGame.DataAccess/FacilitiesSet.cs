using System.Collections.Generic;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.DataAccess
{
    public class FacilitiesSet
    {
        public PlanetFacility RoboticsFactory { get; set; }
        public PlanetFacility Shipyard { get; set; }
        public PlanetFacility ResearchLab { get; set; }
        public PlanetFacility NaniteFactory { get; set; }

    }

    public class PlanetFacilities : List<PlanetFacilities>
    {

    }
}