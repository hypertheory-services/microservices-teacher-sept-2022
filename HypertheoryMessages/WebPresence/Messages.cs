namespace HypertheoryMessages.WebPresence;

public record WebUser
{
    public string UserId { get; init; } = string.Empty;
    public string FirstName { get; init; } = String.Empty;
    public string LastName { get; init; } = string.Empty;
    public string EMail { get; init; } = String.Empty;
    public DateTime OnboardedDate { get; init; }
    public static readonly string Topic = "hypertheory.webpresence.user-onboarded";
}

public record RegistrationRequest
{
    public string Id { get; init; } = String.Empty;
    public string CourseId { get; init; } = string.Empty;
    public string OfferingId { get; init; } = String.Empty;
    
    public string UserId { get; init; } = String.Empty;
    public DateTime RequestedAt { get; init; } 
    public static readonly string Topic = "hypertheory.webpresence.registration-requested";
}