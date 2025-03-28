using ElasticMongoDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElasticMongoDemo.Application.Interfaces
{
    public interface IProductService 
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
        Task<string> CreateAsync(Product product);
        Task<string> UpdateAsync(string id, Product product);
        Task <string> DeleteAsync(string id);
        Task<List<Product>> FindAsync(Expression<Func<Product, bool>> predicate);
        Task<(Product mongoProduct, Product elasticProduct, string timing)> ComparePerformanceAsync(string id);
    }
}
