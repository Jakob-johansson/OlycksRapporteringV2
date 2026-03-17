using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Application.Services;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Infrastructure.Repositories;
using OlycksRapporteringV2.MAUI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OlycksRapporteringV2.MAUI.ViewModels
{
    public class AdminNotificationsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        //Design pattern: Facade
        //Här använder jag Facade för att den här klassen inte behöver känna till flera
        //repositories. Då använder vi ReportService.cs som hanterar det istället.

        private readonly ReportService _reportService;
        private readonly INotificationRepository _notificationRepo;

        //LISTOR\\
        public ObservableCollection<Notification> Notifications { get; set; } = new();
        public bool HasNoNotifications => !Notifications.Any();

        //KONSTRUKTOR\\
        public AdminNotificationsPageViewModel()
        {
            _notificationRepo = new NotificationRepositoryDb();
            _reportService = new ReportService();
        }

        //HÄMTA NOTISER\\
        public async Task LoadNotifications()
        {
            var notifications = await _notificationRepo.GetAdminNotifications();
            Notifications.Clear();
            foreach (var notification in notifications)
                Notifications.Add(notification);

            OnPropertyChanged(nameof(HasNoNotifications));
        }

        //MARKERA SOM LÄST\\
        public async Task MarkAsRead(Notification notification)
        {
            notification.IsRead = true;
            await _notificationRepo.MarkAsRead(notification.Id);
            OnPropertyChanged(nameof(Notifications));
        }

        //HÄMTA RAPPORT KOPPLAD TILL NOTIS\\
        public async Task<Report> GetReportForNotification(Notification notification)
        {
            return await _reportService.GetReportById(notification.ReportId);
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
