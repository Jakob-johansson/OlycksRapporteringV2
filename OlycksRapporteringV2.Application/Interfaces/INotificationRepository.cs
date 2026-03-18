using OlycksRapporteringV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Application.Interfaces
{
    public interface INotificationRepository
    {
        Task SendNotification(Notification notification);
        Task<List<Notification>> GetAdminNotifications();

        Task MarkAsRead(string notificationId);

        Task<List<Notification>> GetUserNotifications(string userId);
        Task UpdateNotificationStatus(string notificationId, string status);

        Task<Notification> GetPendingEditRequest(string reportId);

        Task UpdateNotificationMessage(string notificationId, string message);

    }
}
