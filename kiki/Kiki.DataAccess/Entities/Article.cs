namespace Kiki.DataAccess.Entities
{
    public partial class Article
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public string Title { get; set; }
        public string TitleEng { get; set; }
        public string Text { get; set; }
        public string TextEng { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
    }
}
