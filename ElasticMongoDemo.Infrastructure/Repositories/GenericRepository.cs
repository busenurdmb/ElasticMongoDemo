using ElasticMongoDemo.Application.Interfaces;
using ElasticMongoDemo.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElasticMongoDemo.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(IOptions<MongoDbSettings> mongoSettings)
        {
            // MongoDB bağlantısı kurulur
            var client = new MongoClient(mongoSettings.Value.ConnectionString);

            // Belirtilen veritabanına bağlan
            var database = client.GetDatabase(mongoSettings.Value.DatabaseName);

            // Belirtilen koleksiyona (collection) bağlan
            _collection = database.GetCollection<T>(mongoSettings.Value.CollectionName);
        }

        public async Task AddAsync(T entity)
        {
            // Yeni belge (document) ekler
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            // _id alanına göre kayıt silinir
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            // Şarta uyan belgeleri getirir
            return await _collection.Find(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            // Tüm kayıtları getirir
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            // Belirli bir _id'ye sahip kaydı getirir
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, T entity)
        {
            // _id'ye göre güncelleme yapar (replace işlemi)
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            await _collection.ReplaceOneAsync(filter, entity);
        }
    }
}
