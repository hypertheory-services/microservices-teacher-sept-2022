using Dapr.Client;
using HypertheoryMessages.WebPresence;
using WebPresenceBFF.Domain;
using RegistrationRequest = HypertheoryMessages.WebPresence.RegistrationRequest;

namespace WebPresenceBFF.Adapters;

public class DaprAdapter
{
    private readonly DaprClient _client;

    public DaprAdapter(DaprClient client)
    {
        _client = client;
    }


    public async Task UserOnboardedAsync(SiteUser user)
    {
        var userCreated = new WebUser
        {
            UserId = user.UserName,
            EMail = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            OnboardedDate = DateTime.Now
        };

        await _client.PublishEventAsync("webpresence", WebUser.Topic, userCreated);
    }

    public async Task RegistrationRequestedAsync(RegistrationEntity request)
    {
        var registrationRequest = new RegistrationRequest
        {
            Id = request.Id,
            CourseId = request.CourseId,
            OfferingId = request.OfferingId,
            RequestedAt = DateTime.Now,
            UserId = request.UserId
        };
        await _client.PublishEventAsync("webpresence", RegistrationRequest.Topic, registrationRequest);
    }
}