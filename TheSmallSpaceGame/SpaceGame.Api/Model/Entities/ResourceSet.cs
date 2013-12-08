using SpaceGame.DataAccess.Entities;

namespace SpaceGame.Api.Model.Entities
{
    public class ResourceSet
    {
        public PlanetResource Metal { get; set; }
        public PlanetResource Crystal { get; set; }
        public PlanetResource Deiterium { get; set; }
    }
}