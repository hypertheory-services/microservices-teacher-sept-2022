using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainingManagement.Domain;

namespace TrainingManagement.Pages.Instructors;

public class IndexModel : PageModel
{
    private readonly InstructorManager _instructorManager;

    public List<InstructorEntity> Instructors { get; set; } = new();
    public IndexModel(InstructorManager instructorManager)
    {
        _instructorManager = instructorManager;
    }

    public async Task OnGetAsync()
    {
        Instructors = await _instructorManager.GetAllInstructorsAsync();
    }
}
