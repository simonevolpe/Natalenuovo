using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using NataleDb = Natale.Classes.MongoDB;
using Natale.Classes;
using System.Linq;
// TODO make tests for exceptions

namespace Natale.Tests.Integration
{
    [TestClass]
    public class Toys
    {
        private IMongoDatabase db;
        private const string TOY_NAME = "test-toy";

        [TestInitialize]
        public void Initialize()
        {
            MongoClientSettings settings = new MongoClientSettings();
            MongoClient client = new MongoClient(MongoDBConfig.ConnectionString);
            db = client.GetDatabase(MongoDBConfig.DBName);
            IMongoCollection<Toy> collection = db.GetCollection<Toy>("toys");
            collection.InsertOne(new Toy
            {
                Name = TOY_NAME
            });
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (db != null)
            {
                db.DropCollection("toys");
            }
        }

        [TestMethod]
        public void GetAllToys_Should_Return_A_List()
        {
            var db = new NataleDb();
            var list = db.GetAllToys();
            Assert.AreEqual(1, list.Count());
        }
    }
}