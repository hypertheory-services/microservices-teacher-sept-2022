using MongoDB.Bson.Serialization.Attributes;

namespace WebPresenceBFF.Domain;


public class RegistrationEntity
{
    [BsonElement("_id")]
    public string Id { get; set; } = String.Empty;
    public string CourseId { get; set; } = string.Empty;
    public string OfferingId { get; set; } = String.Empty;
    public string Status { get; set; } = String.Empty;
    public string UserId { get; set; } = String.Empty;
}

