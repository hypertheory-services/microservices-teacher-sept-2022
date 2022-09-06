using ECommerceApi.Adapters;
using ECommerceApi.Domain;
using ECommerceApi.Models;
using MongoDB.Bson.Serialization.Conventions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var conventionPack = new ConventionPack
{
    new CamelCaseElementNameConvention(),
    new IgnoreExtraElementsConvention(true),
    new EnumRepresentationConvention(MongoDB.Bson.BsonType.String)
};
ConventionRegistry.Register("defaults", conventionPack, t => true);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDaprClient();

// Adapter Services.
var mongoConnectionString = builder.Configuration.GetConnectionString("mongo");
builder.Services.AddSingleton<MongoDbOrdersAdapter>(sp =>
{
    return new MongoDbOrdersAdapter(mongoConnectionString);
});

//builder.Services.AddHttpClient<InventoryHttpAdapter>(client =>
//{
//    client.BaseAddress = new Uri("http://localhost:1338"); // We will fix this in a bit.
//});
builder.Services.AddScoped<InventoryPublisherAdapter>();
builder.Services.AddScoped<OrderProcessor>(); // lazy
//builder.Services.AddScoped</*InventoryAdjustments*/>();


// This is setting up the middleware and request/response pipeline
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
