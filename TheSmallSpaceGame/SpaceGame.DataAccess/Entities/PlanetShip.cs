namespace SpaceGame.DataAccess.Entities
{
    public partial class PlanetShip
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int PlanetId { get; set; }
        public int ShipId { get; set; }
        public virtual Planet Planet { get; set; }
        public virtual Ship Ship { get; set; }
    }
}