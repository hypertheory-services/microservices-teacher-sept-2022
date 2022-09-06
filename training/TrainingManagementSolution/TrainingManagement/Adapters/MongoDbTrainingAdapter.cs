using MongoDB.Driver;
using TrainingManagement.Domain;

namespace TrainingManagement.Adapters;

public class MongoDbTrainingAdapter
{

    public IMongoCollection<CourseEntity> Courses { get; private set; }
    public IMongoCollection<CourseOfferingEntity> Offerings { get; private set; }
    public IMongoCollection<InstructorEntity> Instructors { get; private set; }

    public MongoDbTrainingAdapter(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("training");
        Courses = database.GetCollection<CourseEntity>("courses");
        Offerings = database.GetCollection<CourseOfferingEntity>("offerings");
        Instructors = database.GetCollection<InstructorEntity>("instructors");
    }
}
