using System.Collections;
using System.Collections.Generic;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.Api.Model.Entities
{
    public class ResourceSet:IEnumerable<PlanetResource>
    {
        public PlanetResource Metal { get; set; }
        public PlanetResource Crystal { get; set; }
        public PlanetResource Deiterium { get; set; }


        public IEnumerator<PlanetResource> GetEnumerator()
        {
            yield return Metal;
            yield return Crystal;
            yield return Deiterium;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}