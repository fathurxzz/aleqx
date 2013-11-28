using System;
using System.Collections.Generic;

namespace SpaceGame.Data2.Models
{
    public partial class Planet
    {
        public Planet()
        {
            this.Resources = new List<Resource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
        public virtual User User { get; set; }
    }
}
