namespace StudentSystem_Model.Models
{
  public class Course
  {
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public int CoursePrice { get; set; }
    public ICollection<StudentCourse>? StudentCourses { get; set; }
  }
}
