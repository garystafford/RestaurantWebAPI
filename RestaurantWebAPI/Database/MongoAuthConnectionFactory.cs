using System;
using MongoDB.Driver;

namespace RestaurantWebAPI.Database
{
    internal static class MongoAuthConnectionFactory
    {
        internal static IMongoDatabase MongoDatabase(string databaseName)
        {
            var mongoConnectionString = Environment.GetEnvironmentVariable("MONGO_LOCAL_DB_URL");
            var mongoUsername = Environment.GetEnvironmentVariable("MONGO_LOCAL_DB_USERNAME");
            var mongoPassword = Environment.GetEnvironmentVariable("MONGO_LOCAL_DB_PASSWORD");
            var mongoClient = new MongoClient($"mongodb://{mongoUsername}:{mongoPassword}@{mongoConnectionString}");
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            return mongoDatabase;
        }
    }
}