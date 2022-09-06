using MongoDB.Driver;
using TrainingManagement.Adapters;

namespace TrainingManagement.Domain;

public class InstructorManager
{
    private readonly MongoDbTrainingAdapter _adapter;
    private readonly DaprAdapter _daprAdapter;

    public InstructorManager(MongoDbTrainingAdapter adapter, DaprAdapter daprAdapter)
    {
        _adapter = adapter;
        _daprAdapter = daprAdapter;
    }

    public async Task<List<InstructorEntity>> GetAllInstructorsAsync()
    {
        var filter = Builders<InstructorEntity>.Filter.Where(_ => true);
        return await _adapter.Instructors.Find(filter).ToListAsync();
    }

    public async Task AddInstructorAsync(InstructorEntity instructor)
    {
        await _adapter.Instructors.InsertOneAsync(instructor);
        await _daprAdapter.InstructorCreated(instructor);
    }
}
