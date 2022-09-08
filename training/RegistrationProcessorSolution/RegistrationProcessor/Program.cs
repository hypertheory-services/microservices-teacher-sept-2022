using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using RegistrationProcessor.Adapters;
using RegistrationProcessor.Domain;

using userMessages = HypertheoryMessages.Users.Messages;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDaprClient();
var mongoConnectionString = builder.Configuration.GetConnectionString("mongodb");
builder.Services.AddSingleton<MongoDbRegistrationsAdapter>(sp => new MongoDbRegistrationsAdapter(mongoConnectionString));

var app = builder.Build();

// Have the Dapr sidecar post messages to this for me..
app.MapPost("/incoming/users", async (
    [FromBody] userMessages.User request,
   [FromServices] MongoDbRegistrationsAdapter adapter,
    [FromServices] DaprClient dapr) =>
{

});




app.Run();