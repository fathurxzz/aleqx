using System;
using System.Collections.Generic;

namespace TheSmallSpaceGame.DataAccess.Entities
{
    public partial class User
    {
        public User()
        {
            this.ResourcesLevels = new List<ResourcesLevel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Resources_Id { get; set; }
        public virtual Resource Resource { get; set; }
        public virtual ICollection<ResourcesLevel> ResourcesLevels { get; set; }
    }
}
