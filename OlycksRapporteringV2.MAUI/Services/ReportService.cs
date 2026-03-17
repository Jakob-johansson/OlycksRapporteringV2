using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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
            var notification = new Notification
            {
                Id = Guid.NewGuid().ToString(),
                FromUserId = UserSession.Instance.CurrentUser.EmployeeId,
                ReportId = report.Id,
                ReportTitle = report.ReportTitle,
                Message = $"{UserSession.Instance.CurrentUser.EmployeeId} begär att få ändra rapporten: {report.ReportTitle}",
                CreatedAt = DateTime.Now,
                IsRead = false
            };
            await _notificationRepo.SendNotification(notification);
        }

        public async Task UpdateStatus(string reportId, ReportStatus status)
        {
            await _reportRepo.UpdateReportStatus(reportId, status);
        }

        public async Task<Report> GetReportById (string id)
        {
            return await _reportRepo.GetReportById(id);
        }

        public async Task UpdateReportStatus (string id, ReportStatus status)
        {
            await _reportRepo.UpdateReportStatus(id, status);
        }

    }
}
