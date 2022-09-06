using MongoDB.Bson;

namespace TrainingManagement.Domain;

public class InstructorEntity
{
    public ObjectId Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
}
