using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentSystem_DataAccess.Data;
using StudentSystem_Model.Models;
using StudentSystem_Model.ViewModel;

namespace StudentSystem.Controllers
{
  public class StudentCourseController : Controller
  {
    private readonly ApplicationDbContext _context;
    public StudentCourseController(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IActionResult> Index(FilterEnrollmentDto f)
    {
      var query = _context.StudentCourses
        .Include(sc => sc.Student)
        .Include(sc => sc.Course)
        .AsQueryable();

      if (!string.IsNullOrWhiteSpace(f.StudentName))
      {
        var searchTerm = f.StudentName.Trim();
        query = query.Where(sc =>
          sc.Student!.FirstName.Contains(searchTerm) ||
          sc.Student.LastName.Contains(searchTerm) ||
          (sc.Student.FirstName + " " + sc.Student.LastName).Contains(searchTerm) ||
          (sc.Student.LastName + " " + sc.Student.FirstName).Contains(searchTerm)
        );
      }

      if (!string.IsNullOrWhiteSpace(f.Email))
      {
        var emailTerm = f.Email.Trim();
        query = query.Where(sc =>
          sc.Student!.Email.Contains(emailTerm));
      }

      var filterdStudentIds = await query
        .Select(sc => sc.StudentId)
        .Distinct()
        .ToListAsync();

      if (f.CourseId.HasValue && f.CourseId.Value > 0)
      {
        filterdStudentIds = await query
          .Where(sc => sc.CourseId == f.CourseId.Value)
          .Select(sc => sc.StudentId)
          .Distinct()
          .ToListAsync();
      }

      var studentCourses = await _context.StudentCourses
        .Include(sc => sc.Student)
        .Include(sc => sc.Course)
        .Where(sc => filterdStudentIds.Contains(sc.StudentId))
        .GroupBy(sc => sc.StudentId)
        .Select(g => new
        {
          Student = g.First().Student,
          Courses = g.Select(sc => sc.Course).ToList(),
          EnrolledAt = g.First().EnrolledAt
        })
        .AsNoTracking()
        .ToListAsync();

      ViewBag.Courses = new SelectList(
          await _context.Courses.AsNoTracking().ToListAsync(),
          "CourseId",
          "CourseName",
          f.CourseId
          );

      ViewBag.StudentName = f.StudentName;
      ViewBag.Email = f.Email;
      ViewBag.CourseId = f.CourseId;

      return View(studentCourses);
    }

    [HttpGet]
    public async Task<IActionResult> Enrollment(int studentId)
    {
      var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.StudentId == studentId);
      if (student == null) return NotFound();

      // Get Enrolled Courses For This Student
      var enrolledCourses = await _context.StudentCourses
        .AsNoTracking()
        .Where(sc => sc.StudentId == studentId)
        .Select(c => c.CourseId)
        .ToListAsync();

      var enrolledSet = enrolledCourses.ToHashSet();

      var vm = new EnrollmentVm
      {
        StudentId = studentId,
        StudentName = student.FullName,
        Email = student.Email,
        // Because it's a list of checkbox, we need to create a list of CourseCheckboxVm
        Courses = await _context.Courses
        .AsNoTracking()
        .Select(c => new CourseCheckboxVm
        {
          CourseId = c.CourseId,
          CourseName = c.CourseName,
          // checks if the course is enrolled by the student
          IsSelected = enrolledCourses.Contains(c.CourseId)
        }).ToListAsync()
      };

      return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Enrollment(EnrollmentVm enrollment)
    {
      var couresIds = enrollment.Courses
        .Where(c => c.IsSelected)
        .Select(c => c.CourseId)
        .ToList();

      var existingIds = await _context.StudentCourses
        .Where(sc => sc.StudentId == enrollment.StudentId)
        .Select(sc => sc.CourseId)
        .ToListAsync();

      var coursesToAdd = couresIds.Except(existingIds).ToList();
      var coursesToRemove = existingIds.Except(couresIds).ToList();

      var newEnrollment = coursesToAdd.Select(courseId => new StudentCourse
      {
        StudentId = enrollment.StudentId,
        CourseId = courseId
      });

      await _context.AddRangeAsync(newEnrollment);

      var enrollemntsToRemove = _context.StudentCourses
        .Where(sc =>
           sc.StudentId == enrollment.StudentId &&
          coursesToRemove.Contains(sc.CourseId)
        ).ToList();

      _context.StudentCourses.RemoveRange(enrollemntsToRemove);

      await _context.SaveChangesAsync();

      return RedirectToAction(nameof(Index));
    }
  }
}
