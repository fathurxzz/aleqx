using System.Collections.Generic;

namespace SpaceGame.DataAccess.Entities
{
    public partial class User
    {
        public User()
        {
            this.Planets = new List<Planet>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Planet> Planets { get; set; }
    }
}
