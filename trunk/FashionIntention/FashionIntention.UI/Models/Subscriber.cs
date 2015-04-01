using System;
using System.Collections.Generic;

namespace FashionIntention.UI.Models
{
    public partial class Subscriber
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Guid { get; set; }
    }
}
