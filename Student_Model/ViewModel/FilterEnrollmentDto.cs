using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentSystem_Model.ViewModel
{
  public class FilterEnrollmentDto
  {
    public string? StudentName { get; set; }
    public string? Email { get; set; }
    public int? CourseId { get; set; }
    public IEnumerable<SelectListItem>? Courses { get; set; }
  }
}
