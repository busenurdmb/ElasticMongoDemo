dotnet new sln -n ElasticMongoDemo
dotnet new webapi -n ElasticMongoDemo.API
dotnet new classlib -n ElasticMongoDemo.Application
dotnet new classlib -n ElasticMongoDemo.Domain
dotnet new classlib -n ElasticMongoDemo.Infrastructure

dotnet sln add ElasticMongoDemo.API
dotnet sln add ElasticMongoDemo.Application
dotnet sln add ElasticMongoDemo.Domain
dotnet sln add ElasticMongoDemo.Infrastructure

dotnet add ElasticMongoDemo.API reference ElasticMongoDemo.Application
dotnet add ElasticMongoDemo.API reference ElasticMongoDemo.Domain
dotnet add ElasticMongoDemo.API reference ElasticMongoDemo.Infrastructure
dotnet add ElasticMongoDemo.Application reference ElasticMongoDemo.Domain
dotnet add ElasticMongoDemo.Infrastructure reference ElasticMongoDemo.Domain
