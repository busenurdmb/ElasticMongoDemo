using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElasticMongoDemo.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // Tüm entity sınıfları için geçerli olacak generic bir repository arayüzü
     
            // Tüm kayıtları getir
            Task<List<T>> GetAllAsync();

            // Id'ye göre tek kayıt getir
            Task<T> GetByIdAsync(string id);

            // Şarta göre kayıtları getir
            Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

            // Yeni kayıt ekle
            Task AddAsync(T entity);

            // Kayıt güncelle
            Task UpdateAsync(string id, T entity);

            // Kayıt sil
            Task DeleteAsync(string id);
        }
}
    

