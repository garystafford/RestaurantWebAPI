using MongoDB.Driver;

namespace Restaurant.Order.Database
{
    public interface IMongoConnectionFactory
    {
        IMongoDatabase GetDatabase(string databaseName);
    }
}