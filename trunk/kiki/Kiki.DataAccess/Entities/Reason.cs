namespace Kiki.DataAccess.Entities
{
    public partial class Reason
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public string Title { get; set; }
        public string TitleEng { get; set; }
        public string Text { get; set; }
        public string TextEng { get; set; }
    }
}
