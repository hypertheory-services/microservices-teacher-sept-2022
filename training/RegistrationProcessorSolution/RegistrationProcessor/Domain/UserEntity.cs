namespace RegistrationProcessor.Domain;


public record UserEntity
{
    public string Id { get; init; } = string.Empty;
    public string EMail { get; init; } = string.Empty;
}
