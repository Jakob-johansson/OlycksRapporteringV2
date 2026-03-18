using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Domain.Enums;
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
        Task<List<Report>> GetAllReports();
        Task UpdateReportStatus(string reportId, ReportStatus status);

        Task UpdateReport(Report report);

        Task<Report> GetReportById(string id);
        Task<int> GetTotalReportCount();
        Task<int> GetReportCountByStatus(ReportStatus status);
        Task ArchiveReport(string id);
        Task<List<Report>> GetArchivedReports();

    }
}
