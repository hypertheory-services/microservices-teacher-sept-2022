using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainingManagement.Domain;

namespace TrainingManagement.Pages.Offerings;

public class NewModel : PageModel
{
    private readonly CourseCatalog _courseCatalog;
    private readonly InstructorManager _instructorManager;

    public NewModel(InstructorManager instructorManager, CourseCatalog courseCatalog)
    {
        _instructorManager = instructorManager;
        _courseCatalog = courseCatalog;
    }

    public List<CourseEntity> Courses { get; set; } = new();
    public List<InstructorEntity> Instructors { get; set; } = new();
    [BindProperty]
    public OfferingModel Offering { get; set; } = new();
    public async Task OnGetAsync()
    {
        Courses = await _courseCatalog.GetAllCoursesAsync();
        Instructors = await _instructorManager.GetAllInstructorsAsync();
    }

    public async Task<ActionResult> OnPostAsync()
    {

        await _courseCatalog.AddOfferingAsync(Offering);
        return Redirect("/Offerings/Index");
    }
}

public  class OfferingModel
{
    public string CourseId { get; set; } = "";
    public DateTime StartDate { get; set; } = DateTime.Now;
    public int NumberOfDays { get; set; } = 3;
    public string Location { get; set; } = "";
    public decimal Price { get; set; }
    public string InstructorId { get; set; } = "";
    
}