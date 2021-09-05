using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBotCore.Repositories
{
    public class UserProfileMongoRepository
    {
        IMongoClient _client;
        IMongoCollection<BsonDocument> _collection;
        public UserProfileMongoRepository(IMongoClient cliente)
        {
            this._client = cliente;
            var db = cliente.GetDatabase("db01");
            this._collection = db.GetCollection<BsonDocument>("Col01");
        }
    }
}