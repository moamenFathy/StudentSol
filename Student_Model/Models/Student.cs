using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSystem_Model.Models
{
  public class Student
  {
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    public ICollection<StudentCourse>? StudentCourses { get; set; }
  }
}
