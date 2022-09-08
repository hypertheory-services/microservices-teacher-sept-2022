
namespace HypertheoryMessages.Training;


public record Course
{
    public string CourseId { get; init; } = "";
    public string Title { get; init; } = "";
    public string Description { get; init; } = "";
    public string Category { get; init; } = "";

    public static readonly string Topic = "hypertheory.training.course";
}

public record Instructor
{
    public string Id { get; init; } = "";
    public string FirstName { get; init; } = "";
    public string LastName { get; init; } = "";
    public string Email { get; init; } = "";

    public static readonly string Topic = "hypertheory.training.instructor";
}

public record Offering
{
    public string Id { get; init; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfDays { get; set; }
    public decimal Price { get; set; }
    public string Location { get; init; } = "";
    public string CourseId { get; init; } = "";
    public string InstructorId { get; set; } = "";

    public static readonly string Topic = "hypertheory.training.offering";
}


/*	hypertheory.training.registration
		- Registration Id. (key)
		- created: DateTime
		- userId: jeff@hypertheory.com
		- offeringId: 6318b9dcb410be3025bb6d2d*/

public record Registration
{
    public string Id { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string OfferingId { get; set; } = string.Empty;
    public static readonly string Topic = "hypertheory.training.registration";
}