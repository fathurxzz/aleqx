using System.Collections.Generic;

namespace NexiusFusion.DataAccess.Entities
{
    public partial class Weather
    {
        public Weather()
        {
            this.Reports = new List<Report>();
        }

        public int WeatherId { get; set; }
        public string WeatherDescription { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
