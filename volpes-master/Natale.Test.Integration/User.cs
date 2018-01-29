using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using NataleDb = Natale.Classes.MongoDB;
using Natale.Classes;

namespace Natale.Tests.Integration
{

    // TODO make tests for exceptions

    [TestClass]
    public class Users
    {
        private IMongoDatabase db;
        private const string SCREEN_NAME = "test_user";
        private const string USER_PASSWORD = "test_user";

        [TestInitialize]
        public void Initialize()
        {
            MongoClientSettings settings = new MongoClientSettings();
            MongoClient client = new MongoClient(MongoDBConfig.ConnectionString);
            db = client.GetDatabase(MongoDBConfig.DBName);
            IMongoCollection<User> collection = db.GetCollection<User>("users");
            collection.InsertOne(new User
            {
                ScreenName = SCREEN_NAME,
                Password = USER_PASSWORD
            });
        }
        [TestCleanup]
        public void Cleanup()
        {
            if (db != null)
            {
                db.DropCollection("users");
            }
        }

        [TestMethod]
        public void GetUser_Should_Return_TestUser()
        {
            var db = new NataleDb();
            var test = new User
            {
                ScreenName = SCREEN_NAME,
                Password = USER_PASSWORD
            };
            var user = db.GetUser(test);

            Assert.IsNotNull(user);
        }

    }
}