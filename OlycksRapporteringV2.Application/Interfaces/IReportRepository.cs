using OlycksRapporteringV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Application.Interfaces
{
    public interface IReportRepository
    {
        Task CreateReport(Report report);

        Task<List<Report>> GetReportByUserId(string userId);
        Task DeleteReport(string id);
    }
}
