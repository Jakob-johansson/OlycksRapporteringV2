using MongoDB.Driver;
using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Infrastructure.Repositories
{
    public class ReportRepositoryDb : IReportRepository
    {
        public async Task CreateReport(Report report)
        {
            await MongoDb.GetReportCollection().InsertOneAsync(report);
        }

        public async Task<List<Report>> GetReportByUserId(string userId)
        {
            var filter = Builders<Report>.Filter.Eq(r => r.CreatedByUserId, userId);
            return await MongoDb.GetReportCollection().Find(filter).ToListAsync();
        }

        public async Task DeleteReport(string id)
        {
            var filter = Builders<Report>.Filter.Eq(r => r.Id, id);
            await MongoDb.GetReportCollection().DeleteOneAsync(filter);
        }
        public async Task<List<Report>> GetAllReports()
        {
            return await MongoDb.GetReportCollection().Find(_ => true).ToListAsync();
        }
        public async Task UpdateReportStatus(string reportId, ReportStatus status)
        {
            var filter = Builders<Report>.Filter.Eq(r => r.Id, reportId);
            var update = Builders<Report>.Update.Set(r => r.Status, status);
            await MongoDb.GetReportCollection().UpdateOneAsync(filter, update);
        }
        public async Task UpdateReport(Report report)
        {
            var filter = Builders<Report>.Filter.Eq(r => r.Id, report.Id);
            await MongoDb.GetReportCollection().ReplaceOneAsync(filter, report);
        }

        public async Task<Report> GetReportById(string id)
        {
            var filter = Builders<Report>.Filter.Eq(r => r.Id, id);
            return await MongoDb.GetReportCollection().Find(filter).FirstOrDefaultAsync();
        }
        public async Task<int> GetTotalReportCount()
        {
            return (int)await MongoDb.GetReportCollection().CountDocumentsAsync(_ => true);

        }
        public async Task<int>GetReportCountByStatus(ReportStatus status)
        {
            var filter = Builders<Report>.Filter.Eq(r => r.Status, status);
            return (int)await MongoDb.GetReportCollection().CountDocumentsAsync(filter);
        }
    }
}
