using Messages = HypertheoryMessages.Training;
using MongoDB.Driver;
using WebPresenceBFF.Adapters;
using Microsoft.AspNetCore.SignalR;
using WebPresenceBFF.Hubs;
using HypertheoryMessages.Training;

namespace WebPresenceBFF.Domain;

public class CourseCatalog
{
    private readonly MongoDbBffAdapter _adapter;
    private readonly IHubContext<CoursesHub> _hub;
    private readonly ILogger<CourseCatalog> _logger;

    public CourseCatalog(MongoDbBffAdapter adapter, IHubContext<CoursesHub> hub, ILogger<CourseCatalog> logger)
    {
        _adapter = adapter;
        _hub = hub;
        _logger = logger;
    }

    public async Task<CollectionModel<Course>> GetCoursesAsync()
    {
        var offeringsProjection = Builders<CourseEntity>.Projection.Expression(c => new Course(c.CourseId, c.Title, c.Description, c.Category, c.Offerings.Select(o => new CourseOffering(o.Id, o.StartDate, o.EndDate, o.Location, o.NumberOfDays, o.Price)).ToList()));
    
        var data = await _adapter.Courses.Find(_ => true).Project(offeringsProjection).ToListAsync();
        return new CollectionModel<Course> { Data = data };

    }

    public async Task<CollectionModel<RegistrationEntity>> GetRegistrationsForUserAsync(string userId)
    {
        var filter = Builders<RegistrationEntity>.Filter.Where(r => r.UserId == userId);

        var data = await _adapter.Registrations.Find(filter).ToListAsync();
        return new CollectionModel<RegistrationEntity>() { Data = data };
    }

    public async Task AddCourseAsync(Messages.Course course)
    {
        var courseToAdd = new CourseEntity
        {
            CourseId = course.CourseId,
            Title = course.Title,
            Category = course.Category,
            Description = course.Description,
            Offerings = new List<CourseOfferingEntity>(),

        };
        var filter = Builders<CourseEntity>.Filter.Where(c => c.CourseId == courseToAdd.CourseId);
        var result = await _adapter.Courses.ReplaceOneAsync(filter, courseToAdd, new ReplaceOptions { IsUpsert = true });

        await PublishCourseAsync(course.CourseId);

    }

    public async Task AddCourseOfferingAsync(Messages.Offering offering)
    {
        var offeringToAdd = new CourseOfferingEntity
        {
            Id = offering.Id,
            StartDate = offering.StartDate,
            EndDate = offering.EndDate,
            Location = offering.Location,
            NumberOfDays = offering.NumberOfDays,
            Price = offering.Price
        };

        var filter = Builders<CourseEntity>.Filter.Where(c => c.CourseId == offering.CourseId);

        var update = Builders<CourseEntity>.Update.Push(c => c.Offerings, offeringToAdd);

        await _adapter.Courses.FindOneAndUpdateAsync(filter, update);
        await PublishCourseAsync(offering.CourseId);
       
    }

    private async Task PublishCourseAsync(string courseId)
    {
        var filter = Builders<CourseEntity>.Filter.Where(c => c.CourseId == courseId);
        var offeringsProjection = Builders<CourseEntity>.Projection.Expression(c => new Course(c.CourseId, c.Title, c.Description, c.Category, c.Offerings.Select(o => new CourseOffering(o.Id, o.StartDate, o.EndDate, o.Location, o.NumberOfDays, o.Price)).ToList()));
        var courseAdded = await _adapter.Courses.Find(filter).Project(offeringsProjection).SingleOrDefaultAsync();
        if (courseAdded != null)
        {
            _logger.LogInformation($"Got a course, sending to connected clients {courseAdded.Title}");

            await _hub.Clients.All.SendAsync("course", courseAdded);
        }
    }

}
