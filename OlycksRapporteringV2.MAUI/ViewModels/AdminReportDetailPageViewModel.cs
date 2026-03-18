using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Application.Services;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.Infrastructure.Repositories;
using OlycksRapporteringV2.MAUI.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MauiApp = Microsoft.Maui.Controls.Application;

namespace OlycksRapporteringV2.MAUI.ViewModels
{
    public class AdminReportDetailPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // DESIGN PATTERN: FACADE
        // Använder ReportService så denna klass inte behöver
        // känna till alla repositories direkt.
        private readonly ReportService _reportService;
        private readonly IIncidentRepository _incidentRepo;
        private readonly NotificationRepositoryDb _notificationRepo;

        public bool IsArchived => Report?.IsArchived ?? false;


        //RAPPORTEN\\
        private Report _report;
        public Report Report
        {
            get => _report;
            set { _report = value; OnPropertyChanged(); }
        }

        //AKTIV STATUS\\
        private ReportStatus _activeStatus;
        public ReportStatus ActiveStatus
        {
            get => _activeStatus;
            set { _activeStatus = value; OnPropertyChanged(); }
        }

        
        public AdminReportDetailPageViewModel(Report report)
        {
            _reportService = new ReportService();
            _incidentRepo = new IncidentRepositoryDb();
            _notificationRepo = new NotificationRepositoryDb();
            Report = report;
            ActiveStatus = report.Status;
        }
        public async Task ArchiveReport()
        {
            await _reportService.ArchiveReport(Report.Id);
            await MauiApp.Current.MainPage.DisplayAlertAsync(
                "Arkiverad", "Rapporten har arkiverats.", "OK");
        }
        //SÄTT STATUS\\
        public async Task SetStatus(ReportStatus status)
        {
            await _reportService.UpdateStatus(Report.Id, status);
            Report.Status = status;
            ActiveStatus = status;
            OnPropertyChanged(nameof(Report));

            //UPPDATERA ANVÄNDARENS NOTIS\\
            var notificationStatus = status == ReportStatus.Created ? "Approved" : "Denied";
            var userNotification = await _notificationRepo.GetPendingEditRequest(Report.Id);

            if (userNotification != null)
            {
                var newMessage = status == ReportStatus.Created
                    ? $"✅ Admin godkände din förfrågan att redigera '{Report.ReportTitle}'"
                    : $"❌ Admin nekade din förfrågan att redigera '{Report.ReportTitle}'";

                await _notificationRepo.UpdateNotificationStatus(userNotification.Id, notificationStatus);
                await _notificationRepo.UpdateNotificationMessage(userNotification.Id, newMessage);
            }

            await MauiApp.Current.MainPage.DisplayAlertAsync(
                "Status uppdaterad",
                $"Rapporten är nu markerad som: {status}",
                "OK");
        }

        //GODKÄNN RAPPORT OCH SKAPA HÄNDELSE\\
        public async Task ApproveWithIncident(Priority priority, string title, string description, bool notifyAll)
        {
            //UPPDATERA RAPPORTSTATUS\\
            await _reportService.UpdateStatus(Report.Id, ReportStatus.Approved);
            Report.Status = ReportStatus.Approved;
            Report.PriorityLevel = priority;
            ActiveStatus = ReportStatus.Approved;
            OnPropertyChanged(nameof(Report));

            //SKAPA HÄNDELSE\\
            var incident = new Incident
            {
                Id = Guid.NewGuid().ToString(),
                Title = title,
                Description = description,
                Priority = priority,
                CreatedAt = DateTime.Now,
                CreatedByAdminId = UserSession.Instance.CurrentUser.EmployeeId,
                LinkedReportId = Report.Id,
                IsActive = true,
                NotifyAll = notifyAll
            };

            await _incidentRepo.CreateIncident(incident);

            await MauiApp.Current.MainPage.DisplayAlertAsync(
                "Godkänd",
                $"Rapporten är godkänd och händelsen har skapats med prioritet: {priority}",
                "OK");
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    
    }