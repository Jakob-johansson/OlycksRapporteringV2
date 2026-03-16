using MongoDB.Driver;
using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Infrastructure.Repositories
{
    public class UserRepositoryDb : IUserRepository
    {
        public async Task<User> GetUserByEmployeeId(string employeeId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.EmployeeId, employeeId);
            return await MongoDb.GetUserCollection().Find(filter).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(e => e.Email, email);
            return await MongoDb.GetUserCollection().Find(filter).FirstOrDefaultAsync();
        }
        public async Task OnClickedCreateUser(User user)
        {
            await MongoDb.GetUserCollection().InsertOneAsync(user);
        }

        //När man loggar in så gör denna funktion så man kan använda både email eller användarnamn, 
        //den kollar efter båda istället för att man ska välja.
        public async Task<User> GetUserByEmployeeIdOrEmail(string identifier)
        {
            var filter = Builders<User>.Filter.Or(
                Builders<User>.Filter.Eq(a => a.EmployeeId, identifier),
                Builders<User>.Filter.Eq(a => a.Email, identifier));
            return await MongoDb.GetUserCollection().Find(filter).FirstOrDefaultAsync();
        }
        public async Task UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
            await MongoDb.GetUserCollection().ReplaceOneAsync(filter, user);
        }

        public async Task<User> GetUserById(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return await MongoDb.GetUserCollection().Find(filter).FirstOrDefaultAsync();
        }

    }
}
