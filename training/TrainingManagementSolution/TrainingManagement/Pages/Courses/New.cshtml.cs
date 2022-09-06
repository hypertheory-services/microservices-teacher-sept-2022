using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainingManagement.Adapters;
using TrainingManagement.Domain;

namespace TrainingManagement.Pages.Courses;

public class NewModel : PageModel
{
    private readonly CourseCatalog _catalog;

    public NewModel(CourseCatalog catalog)
    {
        _catalog = catalog;
    }

    [BindProperty]
    public CourseEntity Course { get; set; } = new();
    public void OnGet()
    {
    }

    public async Task<ActionResult> OnPostAsync()
    {
       await _catalog.AddCourseAsync(Course);
        return Redirect("/Courses/Index");
    }
}
