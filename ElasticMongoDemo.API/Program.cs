using ElasticMongoDemo.Application.Interfaces;
using ElasticMongoDemo.Application.Services;
using ElasticMongoDemo.Infrastructure.Providers;
using ElasticMongoDemo.Infrastructure.Repositories;
using ElasticMongoDemo.Infrastructure.Services;
using ElasticMongoDemo.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// ?? Swagger servislerini ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(); // <-- Eksik olan satýr bu!

// ?? MongoDB ayarlarýný konfigürasyon üzerinden baðla
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// ?? Generic repository ve servisleri kaydet
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.Configure<ElasticsearchSettings>(
    builder.Configuration.GetSection("ElasticsearchSettings"));

//builder.Services.AddSingleton<IElasticClient>(sp =>
//{
//    var settings = sp.GetRequiredService<IOptions<ElasticsearchSettings>>().Value;
//    var connectionSettings = new ConnectionSettings(new Uri(settings.Uri))
//        .DefaultIndex(settings.IndexName);
//    return new ElasticClient(connectionSettings);
//});

builder.Services.AddSingleton<IElasticClient>(sp =>
{
    var provider = sp.GetRequiredService<ElasticClientProvider>();
    return provider.GetClient(); // Ýstemciyi dýþarý veriyoruz
});

builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<ElasticsearchSettings>>().Value;
    return new ElasticClientProvider(settings.Uri,settings.IndexName); // senin yazdýðýn sýnýf
});
// ? 2. IElasticClient isteyen servislere doðrudan çözüm saðla

builder.Services.AddScoped<IElasticService, ElasticService>();
var app = builder.Build();

// ?? Swagger arayüzünü aktif et
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Swagger JSON
    app.UseSwaggerUI(); // Swagger UI
}

// ?? HTTPS yönlendirme
app.UseHttpsRedirection();

app.MapControllers(); // <-- Bu satýr çok önemli!

app.Run();

