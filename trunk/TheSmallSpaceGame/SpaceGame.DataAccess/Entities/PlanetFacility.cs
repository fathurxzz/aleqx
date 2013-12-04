namespace SpaceGame.DataAccess.Entities
{
    public partial class PlanetFacility
    {
        public int Id { get; set; }
        public short Level { get; set; }
        public System.DateTime UpdateStart { get; set; }
        public System.DateTime UpdateFinish { get; set; }
        public bool IsUpdating { get; set; }
        public int PlanetId { get; set; }
        public int FacilityId { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual Planet Planet { get; set; }
    }
}