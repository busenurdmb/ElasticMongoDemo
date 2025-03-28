using ElasticMongoDemo.Application.Interfaces;
using ElasticMongoDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElasticMongoDemo.Application.Services
{
    public class ProductService:IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IElasticService _elasticService; // ✅ Elasticsearch servisini al

        public ProductService(IGenericRepository<Product> repository, IElasticService elasticService)
        {
            _repository = repository;
            _elasticService = elasticService;
        }


        public async Task<List<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<string> CreateAsync(Product product)
        {
            await _repository.AddAsync(product);
            await _elasticService.IndexProductAsync(product); // Elasticsearch'e de ekle
            return "✅ Ürün MongoDB ve Elasticsearch'e eklendi.";
        }

        public async Task<string> UpdateAsync(string id, Product product)
        {
            await _repository.UpdateAsync(id, product);
            await _elasticService.DeleteProductAsync(id); // Eskiyi sil
            await _elasticService.IndexProductAsync(product); // Yeniyi ekle
            return "✅ Ürün güncellendi ve Elasticsearch indekslendi.";
        }

        public async Task<string> DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
            await _elasticService.DeleteProductAsync(id);
            return "🗑️ Ürün hem MongoDB'den hem Elasticsearch'ten silindi.";
        }
        public async Task<List<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }
        public async Task<(Product mongoProduct, Product elasticProduct, string timing)> ComparePerformanceAsync(string id)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Start();
            var mongoProduct = await _repository.GetByIdAsync(id);
            stopwatch.Stop();
            var mongoMs = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            var elasticProduct = await _elasticService.GetProductByIdAsync(id);
            stopwatch.Stop();
            var elasticMs = stopwatch.ElapsedMilliseconds;

            string result = $"⏱ MongoDB: {mongoMs} ms | Elasticsearch: {elasticMs} ms";
            return (mongoProduct, elasticProduct, result);
        }

    }
}
