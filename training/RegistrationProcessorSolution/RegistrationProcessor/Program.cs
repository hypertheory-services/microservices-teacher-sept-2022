using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RegistrationProcessor.Adapters;
using RegistrationProcessor.Domain;

using userMessages = HypertheoryMessages.Users.Messages;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDaprClient();
var mongoConnectionString = builder.Configuration.GetConnectionString("mongodb");
builder.Services.AddSingleton<MongoDbRegistrationsAdapter>(sp => new MongoDbRegistrationsAdapter(mongoConnectionString));

var app = builder.Build();

app.UseCloudEvents();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapSubscribeHandler());

// Have the Dapr sidecar post messages to this for me..
app.MapPost("/incoming/users", async (
    [FromBody] userMessages.User request,
    [FromServices] MongoDbRegistrationsAdapter adapter) =>
{
    var user = new UserEntity
    {
        Id = request.UserId,
        EMail = request.EMail
    };

    var filter = Builders<UserEntity>.Filter.Where(u => u.Id == user.Id);

    await adapter.Users.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });

    return Results.Ok();

}).WithTopic("registration-processor", userMessages.User.Topic);




app.Run();