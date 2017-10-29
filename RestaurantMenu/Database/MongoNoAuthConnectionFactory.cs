using MongoDB.Driver;

namespace Restaurant.Menu.Database
{
    public class MongoNoAuthConnectionFactory : IMongoConnectionFactory
    {
        public IMongoDatabase GetDatabase(string databaseName)
        {
            var mongoClient = new MongoClient($"mongodb://localhost:27017");
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            return mongoDatabase;
        }
    }
}