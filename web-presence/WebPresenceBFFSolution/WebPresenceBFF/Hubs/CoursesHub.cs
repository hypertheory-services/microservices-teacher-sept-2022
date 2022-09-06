using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using WebPresenceBFF.Adapters;
using WebPresenceBFF.Domain;

namespace WebPresenceBFF.Hubs;

[Authorize]
public class CoursesHub : Hub
{
    private readonly ILogger<CoursesHub> _logger;
    private readonly MongoDbBffAdapter _adapter;
    private readonly DaprAdapter _dapr;
    public CoursesHub(ILogger<CoursesHub> logger, MongoDbBffAdapter adapter, DaprAdapter dapr)
    {
        _logger = logger;
        _adapter = adapter;
        _dapr = dapr;
    }

    public async Task RegistrationRequested(RegistrationEntity registrationRequest)
    {
        registrationRequest.UserId = Context.User!.Identity!.Name!;
        registrationRequest.Status = "pending";
        await _adapter.Registrations.InsertOneAsync(registrationRequest);
        await _dapr.RegistrationRequestedAsync(registrationRequest);
        await Clients.Caller.SendAsync("registration", registrationRequest);

    }
}

