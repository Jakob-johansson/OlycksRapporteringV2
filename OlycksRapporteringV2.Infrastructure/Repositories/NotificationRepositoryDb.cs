using MongoDB.Driver;
using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Infrastructure.Repositories
{
    public class NotificationRepositoryDb : INotificationRepository
    {

        public async Task SendNotification(Notification notification)
        {
            await MongoDb.GetNotificationCollection().InsertOneAsync(notification);
        }

        public async Task<List<Notification>> GetAdminNotifications()
        {
            return await MongoDb.GetNotificationCollection().Find(_ => true).ToListAsync();
        }
        public async Task MarkAsRead(string notificationId)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.Id, notificationId);
            var update = Builders<Notification>.Update.Set(n => n.IsRead, true);
            await MongoDb.GetNotificationCollection().UpdateOneAsync(filter, update);
        }
    }
}
