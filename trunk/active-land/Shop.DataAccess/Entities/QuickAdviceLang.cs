using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    public partial class QuickAdviceLang
    {
        public int Id { get; set; }
        public int QuickAdviceId { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public virtual Language Language { get; set; }
        public virtual QuickAdvice QuickAdvice { get; set; }
    }
}
