using MongoDB.Driver;

namespace Natale.Tests.Mocks
{
    class MongoConnectionMock
    {
        public string DBName { get; set; }
        public IMongoDatabase GetDatabase()
        {
            MongoClientSettings settings = new MongoClientSettings();
            MongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase(DBName);
            return db;
        }
    }
}
