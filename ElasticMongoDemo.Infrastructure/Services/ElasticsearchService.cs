using ElasticMongoDemo.Application.Interfaces;
using ElasticMongoDemo.Domain.Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticMongoDemo.Infrastructure.Services
{
    public class ElasticService : IElasticService
    {
       
            private readonly IElasticClient _elasticClient;

            public ElasticService(IElasticClient elasticClient)
            {
                _elasticClient = elasticClient;
            }
   

        public async Task<List<Product>> SearchProductsAsync(string keyword)
        {
            var response = await _elasticClient.SearchAsync<Product>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.ProductName)
                        .Query(keyword)
                        .Fuzziness(Fuzziness.Auto) // kelime hatalarını da bulur
                    )
                )
            );

            return response.Documents.ToList();
        }

        //🔍 Bu metot, ürün adı ve açıklaması içinde geçen kelimelere göre ürünleri getirir.
        public async Task<List<Product>> SearchAsync(string keyword)
        {
            var response = await _elasticClient.SearchAsync<Product>(s => s
                .Query(q => q
                    .MultiMatch(m => m
                        .Fields(f => f
                            .Field(p => p.ProductName)
                            .Field(p => p.ProductDescription)
                        )
                        .Query(keyword)
                    )
                )
            );

            if (!response.IsValid)
                throw new Exception("Search failed: " + response.OriginalException.Message);

            return response.Documents.ToList();
        }

        // Elasticsearch’e yeni bir ürün indexleme
        public async Task IndexProductAsync(Product product)
            {
                var response = await _elasticClient.IndexDocumentAsync(product);
                if (!response.IsValid)
                {
                    throw new Exception("Elasticsearch indexing failed: " + response.OriginalException.Message);
                }
            }

            // ID'ye göre ürün getir
            public async Task<Product> GetProductByIdAsync(string id)
            {
                var response = await _elasticClient.GetAsync<Product>(id);
                return response.Source;
            }

            // Elasticsearch’ten ürün sil
            public async Task DeleteProductAsync(string id)
            {
                await _elasticClient.DeleteAsync<Product>(id);
            }

            // Tüm ürünleri getir (Opsiyonel)
            public async Task<IEnumerable<Product>> GetAllProductsAsync()
            {
                var response = await _elasticClient.SearchAsync<Product>(s => s
                    .MatchAll()
                    .Size(1000) // max kaç kayıt
                );
                return response.Documents;
            }
        }
    }
    

