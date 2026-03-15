using MongoDB.Driver;
using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Domain.Entities;
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

    }
}
