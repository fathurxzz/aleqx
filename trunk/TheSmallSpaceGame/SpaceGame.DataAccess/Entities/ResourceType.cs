using System.Collections.Generic;

namespace SpaceGame.DataAccess.Entities
{
    public partial class ResourceType
    {
        public ResourceType()
        {
            this.Resources = new List<Resource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
