using BackendBase.settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BackendBase.Data
{
    public class MongoDataContext
    {
        private IMongoDatabase _database { get; set; }
        private IMongoClient _client { get; set; }

        public MongoDataContext(IOptions<DatabaseSettings> databaseSettings)
        {
            _client = new MongoClient(databaseSettings.Value.ConnectionString);
            _database = _client.GetDatabase(databaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
