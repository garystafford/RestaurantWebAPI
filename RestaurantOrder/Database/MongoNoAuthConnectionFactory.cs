using MongoDB.Driver;

namespace Restaurant.Order.Database
{
    public static class MongoNoAuthConnectionFactory
    {
        public static IMongoDatabase GetDatabase(string databaseName)
        {
            var mongoClient = new MongoClient($"mongodb://localhost:27017");
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            return mongoDatabase;
        }
    }
}