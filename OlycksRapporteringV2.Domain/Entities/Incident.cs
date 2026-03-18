using OlycksRapporteringV2.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Domain.Entities
{
    public class Incident
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByAdminId { get; set; }
        public string LinkedReportId { get; set; }  // kopplar till rapporten
        public bool IsActive { get; set; }           // admin kan avaktivera
        public bool NotifyAll { get; set; }          // alla eller specifika användare
    }
}
