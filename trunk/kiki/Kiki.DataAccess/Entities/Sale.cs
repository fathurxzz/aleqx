namespace Kiki.DataAccess.Entities
{
    public partial class Sale
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEng { get; set; }
        public string Description { get; set; }
        public string DescriptionEng { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string ImageSource { get; set; }
    }
}
