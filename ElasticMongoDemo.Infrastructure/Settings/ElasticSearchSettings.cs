using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticMongoDemo.Infrastructure.Settings
{
    public class ElasticsearchSettings
    {
        public string Uri { get; set; }
        public string IndexName { get; set; }
    }
}
