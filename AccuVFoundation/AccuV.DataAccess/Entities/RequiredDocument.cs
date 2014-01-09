using System;
using System.Collections.Generic;

namespace AccuV.DataAccess.Entities
{
    public partial class RequiredDocument
    {
        public RequiredDocument()
        {
            this.UploadedDocuments = new List<UploadedDocument>();
        }

        public int DocReqId { get; set; }
        public string DocId { get; set; }
        public string ProjectId { get; set; }
        public string SiteNumber { get; set; }
        public string TaskId { get; set; }
        public bool Active { get; set; }
        public string category { get; set; }
        public bool not_applicable { get; set; }
        public DateTime LastModified { get; set; }
        public virtual ICollection<UploadedDocument> UploadedDocuments { get; set; }
    }
}
