using ElasticMongoDemo.Application.Interfaces;

using ElasticMongoDemo.Domain.Entities;
using ElasticMongoDemo.Infrastructure.Seed;
using ElasticMongoDemo.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Diagnostics;

namespace ElasticMongoDemo.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
     public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IElasticService _elasticService;

        public ProductController(IProductService productService, IElasticService elasticService)
        {
            _productService = productService;
            _elasticService = elasticService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products.Count());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var result = await _productService.CreateAsync(product);
            return Ok(new { message = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Product product)
        {
            var result = await _productService.UpdateAsync(id, product);
            return Ok(new { message = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _productService.DeleteAsync(id);
            return Ok(new { message = result });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var results = await _elasticService.SearchAsync(keyword);
            return Ok(results);
        }
        [HttpGet("search2")]
        public async Task<IActionResult> Search2([FromQuery] string keyword)
        {
            var results = await _elasticService.SearchProductsAsync(keyword);
            return Ok(results);
        }
        [HttpPost("seed")]
        public async Task<IActionResult> SeedProducts()
        {
            var fakeProducts = ProductSeeder.GenerateFakeProducts(1000);
            foreach (var product in fakeProducts)
            {
                await _productService.CreateAsync(product);
            }
            return Ok($"{fakeProducts.Count} ürün başarıyla eklendi!");
        }

        [HttpGet("compare/{id}")]
        public async Task<IActionResult> Compare(string id)
        {
            var (mongoProduct, elasticProduct, timing) = await _productService.ComparePerformanceAsync(id);

            return Ok(new
            {
                mongoProduct,
                elasticProduct,
                timing
            });
        }

  
    }
}
