using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollectStickers.Models
{
    public class StickerPresentation
    {
        public short Number { get; set; }
        public bool isNeed { get; set; }
        public bool isFree { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsMatch { get; set; }
    }
}
