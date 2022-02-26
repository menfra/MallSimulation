using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DataAcess.DataServices
{
    public class MongoDataServices : IDataServices
    {
        private readonly IMongoDatabase db;
        private readonly IConfiguration _configuration;

        public MongoDataServices(IConfiguration configuration)
        {
            _configuration = configuration;

            var client = new MongoClient(configuration["MongoConnectionString"]);
            db = client.GetDatabase(configuration["MallService:Settings:DB"]);
        }
        public async Task<T> AddData<T>(T tdata)
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            await collection.InsertOneAsync(tdata);

            return tdata;
        }

        public async Task DeleteData<T>(string Id)
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq("Id", Id);

            await collection.DeleteOneAsync(filter);
        }

        public async Task DeleteDataBulk<T>(List<string> Ids)
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.In("Id", Ids);

            await collection.DeleteManyAsync(filter);
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

        public Task UpSertDataBulk<T>(List<string> Ids, T tdata)
        {
            throw new NotImplementedException();
        }

        //public async Task UpSertDataBulk<T>(List<string> Ids, List<T> tdata)
        //{
        //    var collection = db.GetCollection<T>(typeof(T).Name);
        //    foreach (var id in Ids)
        //    {
        //        var filter = Builders<T>.Filter.Eq("Id", id);
        //        await collection.ReplaceOneAsync(filter, tdata, new ReplaceOptions { IsUpsert = true });
        //    }

        //}
    }
}
