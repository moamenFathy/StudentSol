using Microsoft.EntityFrameworkCore;
using StudentSystem_Model.Models;

namespace StudentSystem_DataAccess.Data
{
  public class ApplicationDbContext : DbContext
  {
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();
    public DbSet<EndUser> Admins => Set<EndUser>();
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });

      modelBuilder.Entity<Student>().HasData(
          new Student { StudentId = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" },
          new Student { StudentId = 2, FirstName = "Moamen", LastName = "Fathy", Email = "Fathy@example.com" },
          new Student { StudentId = 3, FirstName = "Mahmoud", LastName = "Mohamed", Email = "Mahmoud@example.com" }
        );

      modelBuilder.Entity<Course>().HasData(
        new Course { CourseId = 1, CourseName = "Math", CoursePrice = 1500 },
          new Course { CourseId = 2, CourseName = "Data Structure", CoursePrice = 1100 },
          new Course { CourseId = 3, CourseName = "Programming", CoursePrice = 1000 }
        );

      modelBuilder.Entity<StudentCourse>().HasData(
          new StudentCourse { StudentId = 1, CourseId = 1, EnrolledAt = new DateTime(2026, 1, 15), Grade = 85.5m },
          new StudentCourse { StudentId = 1, CourseId = 2, EnrolledAt = new DateTime(2025, 9, 15), Grade = 90.0m },
          new StudentCourse { StudentId = 2, CourseId = 3, EnrolledAt = new DateTime(2025, 12, 15), Grade = 88.0m }
        );
    }
  }
}
