namespace AccuV.UI.Dto
{
    public class ActivityDto
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public int ActivityTypeId { get; set; }
        public string TypeName { get; set; }
        public bool Active { get; set; }
    }
}