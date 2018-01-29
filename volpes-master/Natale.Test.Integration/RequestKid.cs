using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using NataleDb = Natale.Classes.MongoDB;
using Natale.Classes;
using MongoDB.Bson;
using System.Linq;

// TODO Add example of CustomApp.config
// TODO make tests for excpetions


namespace Natale.Tests.Integration
{
    [TestClass]
    public class Orders
    {
        private IMongoDatabase db;
        private string testRequestKidId = ObjectId.GenerateNewId().ToString();
        
        [TestInitialize]
        public void Initialize()
        {
            MongoClientSettings settings = new MongoClientSettings();
            MongoClient client = new MongoClient(MongoDBConfig.ConnectionString);
            db = client.GetDatabase(MongoDBConfig.DBName);
            IMongoCollection<RequestKid> collection = db.GetCollection<RequestKid>("orders");
            collection.InsertOne(new RequestKid
            {
                ID = testRequestKidId
            });
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (db != null)
            {
                db.DropCollection("orders");
            }
        }

        [TestMethod]
        public void GetAllOrders_Should_Return_A_List()
        {
            var db = new NataleDb();
            var list = db.GetAllRequestKid();
            Assert.AreEqual(1, list.Count());
        }

        [TestMethod]
        public void GetOrder_Should_Return_TestOrder()
        {
            var db = new NataleDb();
            var requestKid = db.GetRequest(testRequestKidId);
            Assert.IsNotNull(requestKid);
        }

        [TestMethod]
        public void UpdateStatus_Should_Return_True()
        {
            var db = new NataleDb();
            var request = db.GetRequest(testRequestKidId);
            Assert.IsTrue(db.UpdateStatus(request));
        }

        
    }
}