using ElasticMongoDemo.Domain.Entities;
using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticMongoDemo.Infrastructure.Providers
{
    public class ElasticClientProvider
    {
        private readonly IElasticClient _client;

        public ElasticClientProvider(string uri, string defaultIndex)
        {
            var pool = new SingleNodeConnectionPool(new Uri(uri));
            var settings = new ConnectionSettings(pool)
                .DefaultIndex(defaultIndex)
                .DefaultMappingFor<Product>(m => m
                    .IdProperty(p => p.Id)); // Product sınıfında ID tanımlaması yapar

            _client = new ElasticClient(settings);
        }

        public IElasticClient GetClient() => _client;
    }
}
