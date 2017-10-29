using System;
using MongoDB.Driver;

namespace Restaurant.Order.Database
{
    public class AtlasConnectionFactory : IMongoConnectionFactory
    {
        public IMongoDatabase GetDatabase(string databaseName)
        {
            var atlasCluster = Environment.GetEnvironmentVariable("ATLAS_CLUSTER");
            var atlasUsername = Environment.GetEnvironmentVariable("ATLAS_USERNAME");
            var atlasPassword = Environment.GetEnvironmentVariable("ATLAS_PASSWORD");
            var mongoClient = new MongoClient($"mongodb://{atlasUsername}:{atlasPassword}@{atlasCluster}");
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            return mongoDatabase;
        }
    }
}