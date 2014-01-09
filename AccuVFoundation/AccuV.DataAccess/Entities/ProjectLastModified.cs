using System;

namespace AccuV.DataAccess.Entities
{
    public class ProjectLastModified
    {
        public string ProjectId { get; set; }
        public DateTime LastModified { get; set; }
        public Boolean DumpCreated { get; set; }
    }
}