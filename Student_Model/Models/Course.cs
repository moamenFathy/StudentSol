using System.ComponentModel.DataAnnotations;

namespace StudentSystem_Model.Models
{
  public class Course
  {
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    [Range(100, 10000, ErrorMessage = "Course value must be at least 100 and less than 10000")]
    public int CoursePrice { get; set; }
    public ICollection<StudentCourse>? StudentCourses { get; set; }
  }
}
