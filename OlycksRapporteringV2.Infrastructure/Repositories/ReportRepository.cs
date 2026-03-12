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



    }
}
