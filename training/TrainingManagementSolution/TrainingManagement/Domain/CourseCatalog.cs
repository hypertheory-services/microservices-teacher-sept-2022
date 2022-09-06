using MongoDB.Driver;
using TrainingManagement.Adapters;
using TrainingManagement.Pages.Offerings;

namespace TrainingManagement.Domain;

public class CourseCatalog
{
    private readonly MongoDbTrainingAdapter _adapter;
    private readonly DaprAdapter _daprAdapter;
    public CourseCatalog(MongoDbTrainingAdapter adapter, DaprAdapter daprAdapter)
    {
        _adapter = adapter;
        _daprAdapter = daprAdapter;
    }

    public async Task<List<CourseEntity>> GetAllCoursesAsync(bool showRetired = false)
    {
        var filter = Builders<CourseEntity>.Filter.Where(c => c.Retired == showRetired);

        return await _adapter.Courses.Find(filter).ToListAsync();

    }

    public async Task AddCourseAsync(CourseEntity course)
    {
        await _adapter.Courses.InsertOneAsync(course);
        await _daprAdapter.CourseCreated(course);
    }

    public async Task AddOfferingAsync(OfferingModel offering)
    {
        var offeringEntity = new CourseOfferingEntity
        {
            CourseId = offering.CourseId,
            InstructorId = offering.InstructorId,
            StartDate = offering.StartDate,
            NumberOfDays = offering.NumberOfDays,
            EndDate = offering.StartDate.AddDays(offering.NumberOfDays),
            Location = offering.Location,
            Price = offering.Price

        };

        await _adapter.Offerings.InsertOneAsync(offeringEntity);
        await _daprAdapter.OfferingCreated(offeringEntity);
    }
}
