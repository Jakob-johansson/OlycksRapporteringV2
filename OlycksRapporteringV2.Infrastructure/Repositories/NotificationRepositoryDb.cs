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
            var filter = Builders<Notification>.Filter.Eq(n => n.IsAdminNotification, true);
            return await MongoDb.GetNotificationCollection().Find(filter).ToListAsync();
        }
        public async Task MarkAsRead(string notificationId)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.Id, notificationId);
            var update = Builders<Notification>.Update.Set(n => n.IsRead, true);
            await MongoDb.GetNotificationCollection().UpdateOneAsync(filter, update);
        }

        public async Task<List<Notification>> GetUserNotifications (string userId)
        {
            var filter = Builders<Notification>.Filter.And(
          Builders<Notification>.Filter.Eq(n => n.ToUserId, userId),
          Builders<Notification>.Filter.Eq(n => n.IsAdminNotification, false));
            return await MongoDb.GetNotificationCollection().Find(filter).ToListAsync();

        }
        public async Task UpdateNotificationStatus(string notificationId, string status)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.Id, notificationId);
            var update = Builders<Notification>.Update.Set(n => n.Status, status);
            await MongoDb.GetNotificationCollection().UpdateOneAsync(filter, update);
        }
        public async Task<Notification> GetPendingEditRequest(string reportId)
        {
            var filter = Builders<Notification>.Filter.And(
                Builders<Notification>.Filter.Eq(n => n.ReportId, reportId),
                Builders<Notification>.Filter.Eq(n => n.IsAdminNotification, false),
                Builders<Notification>.Filter.Eq(n => n.Status, "Pending"));
            return await MongoDb.GetNotificationCollection().Find(filter).FirstOrDefaultAsync();
        }
        public async Task UpdateNotificationMessage(string notificationId, string message)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.Id, notificationId);
            var update = Builders<Notification>.Update.Set(n => n.Message, message);
            await MongoDb.GetNotificationCollection().UpdateOneAsync(filter, update);
        }
    }
}
