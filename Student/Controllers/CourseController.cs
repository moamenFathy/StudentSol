using Microsoft.AspNetCore.Mvc;
using StudentSystem_DataAccess.Data;
using StudentSystem_Model.Models;

namespace StudentSystem.Controllers
{
  public class CourseController : Controller
  {
    private readonly ApplicationDbContext _context;

    public CourseController(ApplicationDbContext context) => _context = context;

    public IActionResult Index()
    {
      List<Course> courses = _context.Courses.ToList();
      return View(courses);
    }

    // GET
    public IActionResult Upsert(int? id)
    {
      Course? course = new();

      if (id == null || id == 0)
      {
        // Create
        return View(course);
      }

      course = _context.Courses.FirstOrDefault(u => u.CourseId == id);

      if (course == null)
      {
        return NotFound();
      }
      return View(course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Course course)
    {
      if (ModelState.IsValid)
      {
        if (course.CourseId == 0)
        {
          // Create
          await _context.Courses.AddAsync(course);
        }
        else
        {
          // Update
          _context.Courses.Update(course);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(course);
    }

    public async Task<IActionResult> Delete(int id)
    {
      Course? course = _context.Courses.FirstOrDefault(u => u.CourseId == id);

      if (course == null)
      {
        return NotFound();
      }

      _context.Courses.Remove(course);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
  }
}
