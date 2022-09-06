using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebPresenceBFF.Domain;

public class CourseEntity
{
    [BsonElement("_id")]
    public string CourseId { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public List<CourseOfferingEntity> Offerings { get; set; } = new();

}

public class CourseOfferingEntity
{
    [BsonElement("_id")]
    public string Id { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = "";
    public int NumberOfDays { get; set; } = 0;
    public decimal Price { get; set; }
}
