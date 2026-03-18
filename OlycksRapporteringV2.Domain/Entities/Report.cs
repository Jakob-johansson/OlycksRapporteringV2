using OlycksRapporteringV2.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Domain.Entities
{
    public class Report
    {
        public string? Id { get; set; }
        public string? ReportTitle { get; set; }
        public string? ReportDescription { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReportStatus Status { get; set; }
        public Priority PriorityLevel { get; set; }
        public bool IsVisibleToUser { get; set; }
        public string? Location { get; set; }
        public string? AccidentType { get; set; }
        public string? MaterialDamage { get; set; }
        public string? ImmediateActions { get; set; }
        public string? PreventiveActions { get; set; }
        public string? PeopleInvolved { get; set; }
        public InsuranceCompany Insurance { get; set; }
        public string? TimeOfAccident { get; set; }
        public string? AffectedPersonType { get; set; }
        public string? WeatherDescription { get; set; }
        public double? Temperature { get; set; }
        public double? WindSpeed { get; set; }
        public string? WeatherLocation { get; set; }
        public bool IsArchived { get; set; }

    }
}
