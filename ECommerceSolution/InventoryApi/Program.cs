using EcommerceShared.InventoryMessages;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDaprClient(); // not needed for what we have now, but later...
// Add services to the container.

var app = builder.Build();
app.UseCloudEvents(); // (I'll talk about this in a bit...)
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapSubscribeHandler());

// Configure the HTTP request pipeline.


//         await _dapr.PublishEventAsync("ecommercepubsub", "inventory-request", message);

app.MapPost("/pubsub/inventory-checks", async ([FromBody] InventoryCheckRequest request) =>
{
    foreach (var item in request.Items)
    {
        Console.WriteLine($"Checking item {item.Id}...");
        await Task.Delay(500);
    }
    var response = new InventoryCheckResponse
    {
        Status = "Looks good, Ship it!",
        Ok = true
    };
    return Results.Ok(response);

}).WithTopic("ecommercepubsub", "inventory-request");


app.Run();

