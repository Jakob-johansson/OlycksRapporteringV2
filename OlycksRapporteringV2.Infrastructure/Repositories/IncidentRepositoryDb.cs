using MongoDB.Driver;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Infrastructure.Repositories
{
    public class IncidentRepositoryDb : IIncidentRepository
    {
        public async Task CreateIncident(Incident incident)
        {
            await MongoDb.GetIncidentCollection().InsertOneAsync(incident);
        }

        public async Task<List<Incident>> GetActiveIncidents()
        {
            var filter = Builders<Incident>.Filter.Eq(i => i.IsActive, true);
            return await MongoDb.GetIncidentCollection().Find(filter).ToListAsync();
        }

        public async Task DeactivateIncident(string id)
        {
            var filter = Builders<Incident>.Filter.Eq(i => i.Id, id);
            var update = Builders<Incident>.Update.Set(i => i.IsActive, false);
            await MongoDb.GetIncidentCollection().UpdateOneAsync(filter, update);
        }
    }
}
