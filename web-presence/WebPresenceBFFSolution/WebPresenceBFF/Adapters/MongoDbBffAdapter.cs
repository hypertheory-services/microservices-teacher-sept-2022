using MongoDB.Driver;
using WebPresenceBFF.Domain;

namespace WebPresenceBFF.Adapters;

public class MongoDbBffAdapter
{

    public IMongoCollection<CourseEntity> Courses { get; private set; }
    public IMongoCollection<RegistrationEntity> Registrations { get; private set; }

    public MongoDbBffAdapter(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("bff");
        Courses = database.GetCollection<CourseEntity>("courses");
        Registrations = database.GetCollection<RegistrationEntity>("registrations");
    }
}
