using MongoDB.Driver;
using RegistrationProcessor.Domain;

namespace RegistrationProcessor.Adapters;

public class MongoDbRegistrationsAdapter
{
    public IMongoCollection<UserEntity> Users { get; private set; }
    public IMongoCollection<OfferingEntity> Offerings { get; private set; }

    public MongoDbRegistrationsAdapter(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("registration-processor");
        Users = database.GetCollection<UserEntity>("users");
        Offerings = database.GetCollection<OfferingEntity>("offerings");
    }
}
