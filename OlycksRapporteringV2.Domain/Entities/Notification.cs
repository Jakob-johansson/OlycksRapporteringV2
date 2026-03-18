using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Domain.Entities
{
    public class Notification
    {

        public string Id { get; set; }
        
        public string FromUserId { get; set; }
        public string ReportId { get; set; }
        public string ReportTitle { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }

        public string ToUserId { get; set; }
        public string Status { get; set; }
        public bool IsAdminNotification { get; set; }    
    }              
}
