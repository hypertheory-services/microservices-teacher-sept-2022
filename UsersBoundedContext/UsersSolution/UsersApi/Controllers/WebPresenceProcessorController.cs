using Dapr;
using Dapr.Client;
using messages = HypertheoryMessages.Users.Messages;
using HypertheoryMessages.WebPresence;
using Microsoft.AspNetCore.Mvc;

namespace UsersApi.Controllers;

public class WebPresenceProcessorController : ControllerBase
{
    private readonly DaprClient _client;

    public WebPresenceProcessorController(DaprClient client)
    {
        _client = client;
    }

    [HttpPost("/web-presence-processor/user-onboarded")]
    [Topic("users", "hypertheory.webpresence.user-onboarded")]
    public async Task<ActionResult> MakeAUserOutOfThisWebPerson([FromBody] WebUser request)
    {
        // step 2?
        // write it to a database, or whatever.

        // step 3?
        // if this returns a 200Ok, then it will mark that message as consumed.
        // Anything else (400, 500, whatever) it will not mark it as consumed.
        var messageToPublish = new messages.User
        {
            UserId = request.UserId,
            EMail = request.EMail,
            FirstName = request.FirstName,
            LastName = request.LastName,
            AssignedSalesRep = "bob@hypertheory.com"
        };
        await _client.PublishEventAsync("users", messages.User.Topic, messageToPublish);
        return Ok(); // 200
    }
}
