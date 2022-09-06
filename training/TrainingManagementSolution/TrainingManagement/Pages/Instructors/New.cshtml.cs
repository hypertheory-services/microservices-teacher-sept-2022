using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainingManagement.Domain;

namespace TrainingManagement.Pages.Instructors;

public class NewModel : PageModel
{

    private readonly InstructorManager _instructorManager;

    public NewModel(InstructorManager instructorManager)
    {
        _instructorManager = instructorManager;
    }

    [BindProperty]
    public InstructorEntity Instructor { get; set; } = new();
    public void OnGet()
    {
    }

    public async Task<ActionResult> OnPostAsync()
    {
        await _instructorManager.AddInstructorAsync(Instructor);
        return Redirect("/Instructors/Index");
    }


}
