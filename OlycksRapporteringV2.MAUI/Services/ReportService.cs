using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Domain.Entities;


using OlycksRapporteringV2.Infrastructure.Repositories;

using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.Application.Services;

namespace OlycksRapporteringV2.MAUI.Services
{
    public class ReportService
    {
        private readonly IReportRepository _reportRepo;
        private readonly INotificationRepository _notificationRepo;

        public ReportService()
        {
            _reportRepo = new ReportRepositoryDb();
            _notificationRepo = new NotificationRepositoryDb();
        }


        public async Task CreateReport (Report report)
        {
            await _reportRepo.CreateReport(report);
        }

        public async Task<List<Report>> GetMyReports()
        {
            return await _reportRepo.GetReportByUserId(UserSession.Instance.CurrentUser.Id);
        }

        public async Task<List<Report>> GetAllReports()
        {
            return await _reportRepo.GetAllReports();
        }

        public async Task DeleteReport(string id)
        {
            await _reportRepo.DeleteReport(id);
        }

        public async Task RequestEdit(Report report)
        {
            //NOTIS TILL ADMIN\\
            var adminNotification = new Notification
            {
                Id = Guid.NewGuid().ToString(),
                FromUserId = UserSession.Instance.CurrentUser.EmployeeId,
                ToUserId = "admin",
                ReportId = report.Id,
                ReportTitle = report.ReportTitle,
                Message = $"{UserSession.Instance.CurrentUser.EmployeeId} begär att få ändra rapporten: {report.ReportTitle}",
                CreatedAt = DateTime.Now,
                IsRead = false,
                Status = "Pending",
                IsAdminNotification = true
            };

            //NOTIS TILL ANVÄNDAREN SJÄLV\\
            var userNotification = new Notification
            {
                Id = Guid.NewGuid().ToString(),
                FromUserId = UserSession.Instance.CurrentUser.EmployeeId,
                ToUserId = UserSession.Instance.CurrentUser.Id,
                ReportId = report.Id,
                ReportTitle = report.ReportTitle,
                Message = $"Din förfrågan om att redigera rapporten '{report.ReportTitle}' väntar på svar.",
                CreatedAt = DateTime.Now,
                IsRead = false,
                Status = "Pending",
                IsAdminNotification = false
            };

            await _notificationRepo.SendNotification(adminNotification);
            await _notificationRepo.SendNotification(userNotification);
        }

        public async Task UpdateStatus(string reportId, ReportStatus status)
        {
            await _reportRepo.UpdateReportStatus(reportId, status);
        }

        public async Task<Report> GetReportById (string id)
        {
            return await _reportRepo.GetReportById(id);
        }
       

        public async Task UpdateReport(Report report)
        {
            await _reportRepo.UpdateReport(report);
        }
        
       public async Task <int> GetTotalReportCount()
        {
            return await _reportRepo.GetTotalReportCount();
        }
        public async Task<int> GetReportCountByStatus(ReportStatus status)
        {
            return await _reportRepo.GetReportCountByStatus(status);
        }

        public async Task ArchivedReport(string id)
        {
            await _reportRepo.ArchiveReport(id);
        }
        public async Task<List<Report>> GetArchivedReports()
        {
            return await _reportRepo.GetArchivedReports();
        }
        public async Task ArchiveReport(string id)
        {
            await _reportRepo.ArchiveReport(id);
        }
    }
}
