using System;

namespace AccuVAPI.Dto
{
    // This DTO should match the response of SP - usp_att_closeout_documents_uploaded
    // In addition, it contains other members used internally
    // NOTE: the "suffix" value is the same as "prefix"
    //       but we should also maintain "prefix" because the SPs have a naming confusion between suffix and prefix
    public class DmDocument
    {
        public string file_id { get; set; }
        public string client_file_name { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public string prefix { get; set; }
        public string user_id { get; set; }
        public DateTime last_modified { get; set; }
        public string suffix { get; set; }
        public string error { get; set; }
    }
}
