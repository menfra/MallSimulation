using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.DataServices
{
    public class MongoDataServices : IDataServices
    {
        private readonly IMongoDatabase db;
        public MongoDataServices()
        {
            var client = new MongoClient("");
            db = client.GetDatabase("");
        }
        public async Task AddData<T>(T tdata)
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            await collection.InsertOneAsync(tdata);
        }

        public async Task DeleteData<T>(string Id)
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq("Id", Id);

            await collection.DeleteOneAsync(filter);
        }

        public async Task<List<T>> GetAllData<T>()
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            return (await collection.FindAsync(new BsonDocument())).ToList();
        }

        public async Task<T> GetDataByID<T>(string Id)
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq("Id", Id);

            return (await collection.FindAsync(filter)).FirstOrDefault();
        }

        public async Task UpSertData<T>(string Id, T tdata)
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq("Id", Id);
            await collection.ReplaceOneAsync(filter, tdata, new ReplaceOptions { IsUpsert = true });
        }
    }
}
