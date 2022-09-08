using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RegistrationProcessor.Adapters;
using RegistrationProcessor.Domain;

using userMessages = HypertheoryMessages.Users.Messages;
using offeringMessages = HypertheoryMessages.Training; // weird.
using webPresenceMessages = HypertheoryMessages.WebPresence;
using trainingMessages = HypertheoryMessages.Training;
using Google.Rpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDaprClient();
var mongoConnectionString = builder.Configuration.GetConnectionString("mongodb");
builder.Services.AddSingleton<MongoDbRegistrationsAdapter>(sp => new MongoDbRegistrationsAdapter(mongoConnectionString));

var app = builder.Build();

app.UseCloudEvents();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapSubscribeHandler());

app.MapPost("/incoming/web-presence/registration-requested", async (
    [FromBody] webPresenceMessages.RegistrationRequest request,
    [FromServices] MongoDbRegistrationsAdapter adapter,
    [FromServices] DaprClient dapr) =>
{
    // is there an offering?
    var offeringFilter = Builders<OfferingEntity>.Filter.Where(o => o.Id == request.OfferingId);
    var offering = await adapter.Offerings.Find(offeringFilter).SingleOrDefaultAsync();
    if(offering is null)
    {
        return Results.BadRequest(); // TODO: REplace this with a rejected event.
    }
    var userFilter = Builders<UserEntity>.Filter.Where(u => u.Id == request.UserId);
    var user = await adapter.Users.Find(userFilter).SingleOrDefaultAsync();
    if(user is null)
    {
        return Results.BadRequest(); // TODO: REplace this with a rejected event.
    }
    //if (offering.StartDate.Date > DateTime.Now.Date)
    if(true)
    {
        var message = new trainingMessages.Registration
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.Now,
            OfferingId = request.OfferingId,
            UserId = request.UserId
        };
        await dapr.PublishEventAsync("registration-processor", trainingMessages.Registration.Topic, message);
        return Results.Ok();
    }
    else
    {
        Console.WriteLine("Date out of Range");
        return Results.BadRequest(); // again, reject it.
    }
    // publish!

}).WithTopic("registration-processor", webPresenceMessages.RegistrationRequest.Topic);



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

app.MapPost("/incoming/offerings", async (
    [FromBody] offeringMessages.Offering request,
    [FromServices] MongoDbRegistrationsAdapter adapter) =>
{
    var offering = new OfferingEntity
    {
        Id = request.Id,
        StartDate = request.StartDate
    };

    var filter = Builders<OfferingEntity>.Filter.Where(u => u.Id == offering.Id);

    await adapter.Offerings.ReplaceOneAsync(filter, offering, new ReplaceOptions { IsUpsert = true });

    return Results.Ok();

}).WithTopic("registration-processor", offeringMessages.Offering.Topic);



app.Run();