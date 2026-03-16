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
    }
}
