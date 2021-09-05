using MongoDB.Driver;
using MongoDB.Bson;

namespace SimpleBotCore.Repositories
{
    public class PerguntasMongoRepositorio : IPerguntasRepositorio
    {
        MongoClient client;
        IMongoCollection<BsonDocument> perguntas;
        public PerguntasMongoRepositorio(MongoClient client)
        {
            this.client = client;
            var db = client.GetDatabase("db02");
            var collection = db.GetCollection<BsonDocument>("Perguntas");
            perguntas = collection;
        }

        public void GravarPergunta(string userId, string texto)
        {
            var doc = new BsonDocument() { 
                {"Usuario", userId},
                {"Texto", texto}
            };

            perguntas.InsertOne(doc);
        }
    }
}