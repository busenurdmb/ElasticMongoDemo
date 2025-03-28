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
builder.Services.AddControllers(); // <-- Eksik olan sat�r bu!

// ?? MongoDB ayarlar�n� konfig�rasyon �zerinden ba�la
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
    return provider.GetClient(); // �stemciyi d��ar� veriyoruz
});

builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<ElasticsearchSettings>>().Value;
    return new ElasticClientProvider(settings.Uri,settings.IndexName); // senin yazd���n s�n�f
});
// ? 2. IElasticClient isteyen servislere do�rudan ��z�m sa�la

builder.Services.AddScoped<IElasticService, ElasticService>();
var app = builder.Build();

// ?? Swagger aray�z�n� aktif et
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Swagger JSON
    app.UseSwaggerUI(); // Swagger UI
}

// ?? HTTPS y�nlendirme
app.UseHttpsRedirection();

app.MapControllers(); // <-- Bu sat�r �ok �nemli!

app.Run();

