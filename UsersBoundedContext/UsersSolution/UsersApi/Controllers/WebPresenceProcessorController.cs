using HypertheoryMessages.WebPresence;
using Microsoft.AspNetCore.Mvc;

namespace UsersApi.Controllers;

public class WebPresenceProcessorController : ControllerBase
{

    [HttpPost("/web-presence-processor/user-onboarded")]
    public async Task<ActionResult> MakeAUserOutOfThisWebPerson([FromBody] WebUser)
}
