using baseAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace baseAPI.Data
{

    //Database connection class 
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("User");
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Product");

        
    }
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
