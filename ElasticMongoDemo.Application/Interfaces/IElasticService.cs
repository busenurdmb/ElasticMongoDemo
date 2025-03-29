using ElasticMongoDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticMongoDemo.Application.Interfaces
{
    public interface IElasticService
    {
        Task<List<Product>> SearchByCategoryAsync(string keyword, string category); // + kategori filtresi
        Task<List<Product>> SearchByCategoryOnlyAsync(string category);
        Task<List<Product>> SearchByCategoryOnly1Async(string category);
        Task<List<Product>> SearchProductsAsync(string keyword);
        Task<List<Product>> SearchAsync(string keyword);

        Task IndexProductAsync(Product product);
        Task<Product> GetProductByIdAsync(string id);
        Task DeleteProductAsync(string id);
    }
}
