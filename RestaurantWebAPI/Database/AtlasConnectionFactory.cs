using System;
using MongoDB.Driver;

namespace RestaurantWebAPI.Database
{
    public static class AtlasConnectionFactory
    {
        public static IMongoDatabase MongoDatabase(string databaseName)
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