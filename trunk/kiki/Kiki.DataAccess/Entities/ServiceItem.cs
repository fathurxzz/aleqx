namespace Kiki.DataAccess.Entities
{
    public partial class ServiceItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int SortOrder { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
