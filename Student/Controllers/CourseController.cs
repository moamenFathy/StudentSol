using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem_DataAccess.Data;
using StudentSystem_Model.Models;
using StudentSystem_Model.ViewModel;

namespace StudentSystem.Controllers
{
  public class CourseController : Controller
  {
    private readonly ApplicationDbContext _context;

    public CourseController(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IActionResult> Index(FilterCourseDto f)
    {
      var q = _context.Courses.AsQueryable();

      var searchCourseName = f.CourseName?.Trim();

      if (!string.IsNullOrEmpty(searchCourseName))
      {
        q = q.Where(x => x.CourseName.Contains(searchCourseName));
      }

      if (f.CoursePrice != null && f.CoursePrice > 0)
      {
        q = q.Where(x => x.CoursePrice == f.CoursePrice);
      }

      var list = await q.AsNoTracking().ToListAsync();
      ViewBag.CourseName = f.CourseName;
      ViewBag.CoursePrice = f.CoursePrice;
      return View(list);
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
