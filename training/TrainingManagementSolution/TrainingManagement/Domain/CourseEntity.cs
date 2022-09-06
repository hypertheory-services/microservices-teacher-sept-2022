using MongoDB.Bson;

namespace TrainingManagement.Domain;

public class CourseEntity
{
    public ObjectId Id { get; set; }
    public string CourseId { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";

    public bool Retired { get; set; } = false;
}
