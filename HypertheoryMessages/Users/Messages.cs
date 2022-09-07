
namespace HypertheoryMessages.Users.Messages;


public record User
{
    public string UserId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string EMail { get; init; } = String.Empty;

    public string AssignedSalesRep { get; init; } = string.Empty;

    public static readonly string Topic = "hypertheory.users.user";
}