using Bogus;
using ElasticMongoDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticMongoDemo.Infrastructure.Seed
{
    public static class ProductSeeder
    {
        public static List<Product> GenerateFakeProducts(int count = 1000)
        {
            var faker = new Faker<Product>()
                .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
                .RuleFor(p => p.ProductDescription, f => f.Lorem.Sentence())
                .RuleFor(p => p.ProductImageUrl, f => f.Image.PicsumUrl())
                .RuleFor(p => p.ProductPrice, f => f.Random.Decimal(10, 500))
                .RuleFor(p => p.CategoryId, f => f.Commerce.Categories(1).First());

            return faker.Generate(count);
        }
    }
}
