using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaccinesRegister.API.Entites;

namespace VaccinesRegister.API.Repositories
{
    public class VaccinesRegisterRepository : IVaccinesRegisterRepository
    {
        private const string databaseName = "RegisterDetail";
        private const string collectionName = "vaccineregisterdetails";
        private readonly IMongoCollection<VaccineRegister> vaccineregistercollection;
        private readonly FilterDefinitionBuilder<VaccineRegister> filterBuilder = Builders<VaccineRegister>.Filter;

        public VaccinesRegisterRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            vaccineregistercollection = database.GetCollection<VaccineRegister>(collectionName);
        }

        public async Task CreateDetailAsync(VaccineRegister vaccineRegister)
        {
            await vaccineregistercollection.InsertOneAsync(vaccineRegister);
        }

        public async Task DeleteDetailAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await vaccineregistercollection.DeleteOneAsync(filter);
        }

        public async Task<VaccineRegister> GetRegisteredDetailAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await vaccineregistercollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<VaccineRegister>> GetRegisteredDetailsAsync()
        {
            return await vaccineregistercollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateDetailAsync(VaccineRegister vaccineRegister)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, vaccineRegister.Id);
            await vaccineregistercollection.ReplaceOneAsync(filter, vaccineRegister);
        }
    }
}
