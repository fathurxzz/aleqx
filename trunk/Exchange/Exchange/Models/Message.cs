using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int OfficeId { get; set; }
        public int OfficeRcv { get; set; }
        public int UserId { get; set; }
        public int UserRcv { get; set; }
        public int? ParentId { get; set; }
        public bool Read { get; set; }
        public DateTime Date { get; set; }
        public int? TenderId { get; set; }

        public string OfficeName { get; set; }
        public string OfficeNameRcv { get; set; }
        public string UserName { get; set; }
        public string UserNameRcv { get; set; }
        public int UnreadCount { get; set; }


        public void ReadMessage()
        {
            Read = true;
        }
    }
}