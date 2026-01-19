namespace StudentSystem_Model.ViewModel
{
  public class EnrollmentVm
  {
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<CourseCheckboxVm> Courses { get; set; } = new();
  }
}
