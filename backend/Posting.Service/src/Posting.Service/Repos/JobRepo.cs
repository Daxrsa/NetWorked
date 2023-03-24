using MongoDB.Driver;
using Posting.Service.Models;

namespace Posting.Service.Repos
{
    public class JobRepo : IJobRepo
    {
        private const string collectionName = "jobs";
        private readonly IMongoCollection<Job> dbCollection;
        private readonly FilterDefinitionBuilder<Job> filterBuilder = Builders<Job>.Filter;
        public JobRepo()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Jobs");
            dbCollection = database.GetCollection<Job>(collectionName);
        }
        public async Task<IReadOnlyCollection<Job>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }
        public async Task<Job> GetAsync(Guid id)
        {
            FilterDefinition<Job> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(Job entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await dbCollection.InsertOneAsync(entity);
        }
        public async Task UpdateAsync(Job entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            FilterDefinition<Job> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }
        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<Job> filter = filterBuilder.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}