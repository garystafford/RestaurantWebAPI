using MongoDB.Driver;

namespace Restaurant.Menu.Database
{
    public interface IMongoConnectionFactory
    {
        IMongoDatabase GetDatabase(string databaseName);
    }
}