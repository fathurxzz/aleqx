namespace AccuVAPI.Dto
{
    public class DmRequiredDocument
    {
        public string doc_id { get; set; }
        public int doc_req_id { get; set; }
        public string doc_name { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public int upl_count { get; set; }
        public string upl_files { get; set; }
        public string upl_files_ids { get; set; }
    }
}
