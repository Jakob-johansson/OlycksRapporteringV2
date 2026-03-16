using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Infrastructure.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OlycksRapporteringV2.MAUI.ViewModels
{
    public class AdminNotificationsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly INotificationRepository _notificationRepo;
        private readonly IReportRepository _reportRepo;

        //LISTOR\\
        public ObservableCollection<Notification> Notifications { get; set; } = new();
        public bool HasNoNotifications => !Notifications.Any();

        //KONSTRUKTOR\\
        public AdminNotificationsPageViewModel()
        {
            _notificationRepo = new NotificationRepositoryDb();
            _reportRepo = new ReportRepositoryDb();
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
            return await _reportRepo.GetReportById(notification.ReportId);
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
