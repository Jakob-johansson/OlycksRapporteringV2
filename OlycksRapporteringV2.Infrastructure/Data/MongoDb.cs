using MongoDB.Driver;
using OlycksRapporteringV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static OlycksRapporteringV2.Infrastructure.Data.Sercrets;

namespace OlycksRapporteringV2.Infrastructure.Data
{
    public class MongoDb
    {
        private static readonly MongoClient _client =
            new MongoClient(Secrets.MongoConnectionString);
        private static readonly IMongoDatabase _database =
            _client.GetDatabase("IncidentReportDb");
        public static IMongoCollection<User> GetUserCollection()
        {
            return _database.GetCollection<User>("Users");
        }
        public static IMongoCollection<Report> GetReportCollection()
        {
            return _database.GetCollection<Report>("Reports");
        }



    }
}
