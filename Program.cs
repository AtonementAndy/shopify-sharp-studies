using Microsoft.Extensions.Options;
using ShopifyIntegrationApi.Consigurations;
using ShopifySharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddSingleton<IShopifyService, ShopifyService>();


builder.Configuration.AddUserSecrets<Program>();
builder.Services.Configure<ShopifySettings>(builder.Configuration.GetSection("ShopifySettings"));

builder.Services.AddScoped<IGraphService>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<ShopifySettings>>().Value;
    return new GraphService(settings.ShopUrl, builder.Configuration["ApiKey"]);
});

builder.Services.AddScoped<IProductService>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<ShopifySettings>>().Value;
    return new ProductService(settings.ShopUrl, builder.Configuration["ApiKey"]);
});

//builder.Services.AddScoped<IShopifyService>(sp =>
//{
//    var settings = sp.GetRequiredService<IOptions<ShopifySettings>>().Value;
//    return new ShopifyService(settings.ShopUrl, builder.Configuration["ApiKey"]);
//});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();
app.MapControllers();

app.Run();
