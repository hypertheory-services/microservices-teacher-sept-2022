using Dapr.Client;
using Messages = HypertheoryMessages.Training;
using TrainingManagement.Domain;

namespace TrainingManagement.Adapters;

public class DaprAdapter
{
    private readonly DaprClient _dapr;

    public DaprAdapter(DaprClient dapr)
    {
        _dapr = dapr;
    }

    public async Task CourseCreated(CourseEntity course)
    {
        var courseToPublish = new Messages.Course
        {
            CourseId = course.CourseId,
            Title = course.Title,
            Description = course.Description,
            Category = course.Category,
          
        };

        await _dapr.PublishEventAsync("training", Messages.Course.Topic, courseToPublish);
    }

    public async Task InstructorCreated(InstructorEntity instructor)
    {
        var instructorToPublish = new Messages.Instructor
        {
            Id = instructor.Id.ToString(),
            FirstName = instructor.FirstName,
            LastName = instructor.LastName,
            Email = instructor.Email
        };
        await _dapr.PublishEventAsync("training", Messages.Instructor.Topic, instructorToPublish);
    }

    public async Task OfferingCreated(CourseOfferingEntity offeringEntity)
    {
        var offeringToPublish = new Messages.Offering
        {
           Id = offeringEntity.Id.ToString(),
           CourseId = offeringEntity.CourseId,
           StartDate = offeringEntity.StartDate,
           EndDate = offeringEntity.EndDate,
           NumberOfDays = offeringEntity.NumberOfDays,
           InstructorId = offeringEntity.InstructorId,
           Location = offeringEntity.Location,
           Price = offeringEntity.Price
        };

        await _dapr.PublishEventAsync("training", Messages.Offering.Topic, offeringToPublish);
    }
}
