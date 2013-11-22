using System;
using System.Collections.Generic;

namespace TheSmallSpaceGame.DataAccess.Entities
{
    public partial class ResourcesLevel
    {
        public int Id { get; set; }
        public int MetalLevel { get; set; }
        public int CrystalLevel { get; set; }
        public long DeiteriumLevel { get; set; }
        public int User_Id { get; set; }
        public virtual User User { get; set; }
    }
}
