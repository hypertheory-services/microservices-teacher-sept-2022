namespace RegistrationProcessor.Domain;

public record OfferingEntity
{
    public string Id { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
}