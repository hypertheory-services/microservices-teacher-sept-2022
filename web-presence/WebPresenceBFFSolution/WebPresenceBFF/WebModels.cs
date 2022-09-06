using System.Collections.Generic;

namespace WebPresenceBFF;

public record CollectionModel<T>
{
    public IList<T>? Data { get; init; } 
}

public record Course(string Id, string Title, string Description, string Category, IList<CourseOffering> Offerings);

public record CourseOffering(string Id, DateTime StartDate, DateTime EndDate, string Location, int NumberOfDays, decimal Price);