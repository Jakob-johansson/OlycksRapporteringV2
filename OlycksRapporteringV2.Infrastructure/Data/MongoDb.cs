using MongoDB.Driver;
using OlycksRapporteringV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Infrastructure.Data
{
    public class MongoDb
    {
        private static readonly MongoClient _client =
            new MongoClient("mongodb+srv://jakobjohansson02_db_user:Admin1234@cluster0.01cdgjr.mongodb.net/?appName=Cluster0");
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
