using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainingManagement.Domain;

namespace TrainingManagement.Pages.Courses;

public class IndexModel : PageModel
{
    private readonly CourseCatalog _catalog;

    public IndexModel(CourseCatalog catalog)
    {
        _catalog = catalog;
       
    }

    public List<CourseEntity> Courses { get; set; } = new();
    public async Task OnGetAsync()
    {
        this.Courses = await _catalog.GetAllCoursesAsync();
    }
}
