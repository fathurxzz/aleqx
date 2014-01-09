using System;

namespace AccuV.DataAccess.Entities
{
    public partial class UploadedDocument
    {
        public int DocUplId { get; set; }
        public int DocReqId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Prefix { get; set; }
        public string UserId { get; set; }
        public DateTime LastModified { get; set; }
        public string FileId { get; set; }
        public DocumentStatus Status { get; set; }
        public bool Active { get; set; }
        public string Comment { get; set; }
        public virtual RequiredDocument RequiredDocuments { get; set; }

        public UploadedDocument()
        {
            
        }

        public UploadedDocument(UploadedDocument document)
        {
            DocReqId = document.DocReqId;
            FileName = document.FileName;
            FilePath = document.FilePath;
            Prefix = document.Prefix;
            UserId = document.UserId;
            FileId = document.FileId;
        }

        public static UploadedDocument CreateNewActive(UploadedDocument document)
        {
            return new UploadedDocument(document) {Active = true};
        }
    }
}
