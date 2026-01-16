using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem_DataAccess.Data;
using StudentSystem_Model.Models;

namespace StudentSystem.Controllers
{
  public class StudentCourseController : Controller
  {
    private readonly ApplicationDbContext _context;
    public StudentCourseController(ApplicationDbContext context) => _context = context;
    public IActionResult Index()
    {
      List<StudentCourse> studentCourses = _context.StudentCourses.Include(sc => sc.Student).Include(sc => sc.Course).ToList();
      return View(studentCourses);
    }
  }
}
