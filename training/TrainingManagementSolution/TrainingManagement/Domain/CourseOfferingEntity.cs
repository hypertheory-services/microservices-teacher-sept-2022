using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TrainingManagement.Domain;

public class CourseOfferingEntity
{
    [BsonElement("_id")]
    public ObjectId Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = "";
    public int NumberOfDays { get; set; } = 0;
    public decimal Price { get; set; }
    public string CourseId { get; set; } = "";

    public string InstructorId { get; set; } = "";
}