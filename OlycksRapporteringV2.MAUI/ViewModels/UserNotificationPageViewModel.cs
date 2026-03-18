using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Application.Services;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Infrastructure.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OlycksRapporteringV2.MAUI.ViewModels
{
    public class UserNotificationsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly INotificationRepository _notificationRepo;

        //LISTOR\\
        public ObservableCollection<Notification> Notifications { get; set; } = new();
        public bool HasNoNotifications => !Notifications.Any();

        //KONSTRUKTOR\\
        public UserNotificationsPageViewModel()
        {
            _notificationRepo = new NotificationRepositoryDb();
        }

        //HÄMTA ANVÄNDARENS NOTISER\\
        public async Task LoadNotifications()
        {
            var notifications = await _notificationRepo.GetUserNotifications(
                UserSession.Instance.CurrentUser.Id);
            System.Diagnostics.Debug.WriteLine($"Användare ID: {UserSession.Instance.CurrentUser.Id}");
            System.Diagnostics.Debug.WriteLine($"Antal användarnotiser: {notifications?.Count ?? 0}");
            foreach (var n in notifications)
                System.Diagnostics.Debug.WriteLine($"Notis: {n.Message}, ToUserId: {n.ToUserId}, IsAdmin: {n.IsAdminNotification}");
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

        public void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
