using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem_DataAccess.Data;
using StudentSystem_Model.Models;
using StudentSystem_Model.ViewModel;

namespace StudentSystem.Controllers
{
  public class StudentController : Controller
  {
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index(FilterStudentDto f)
    {
      var q = _context.Students.AsQueryable();

      if (!string.IsNullOrWhiteSpace(f.Search))
      {
        var searchTerm = f.Search.Trim();
        q = q.Where(s =>
        s.FirstName.Contains(searchTerm) ||
        s.LastName.Contains(searchTerm) ||
        (s.FirstName + " " + s.LastName).Contains(searchTerm) ||
        (s.LastName + " " + s.FirstName).Contains(searchTerm) ||
        s.Email.Contains(searchTerm)
      );
      }

      var items = await q.AsNoTracking().ToListAsync();
      ViewBag.SearchTerm = f.Search;
      return View(items);
    }

    //GET
    public IActionResult Upsert(int? id)
    {
      Student? student = new Student();
      if (id == null || id == 0)
      {
        // Create
        return View(student);
      }

      student = _context.Students.FirstOrDefault(u => u.StudentId == id);

      if (student == null) return NotFound();

      return View(student);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Student student)
    {
      // Remove validation errors for navigation properties
      //ModelState.Remove("StudentCourses");

      if (ModelState.IsValid)
      {
        if (student.StudentId == 0)
        {
          // Create
          await _context.Students.AddAsync(student);
        }
        else
        {
          // Update
          _context.Students.Update(student);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(student);
    }

    public async Task<IActionResult> Delete(int id)
    {
      Student? student = _context.Students.FirstOrDefault(u => u.StudentId == id);

      if (student == null)
        return NotFound();

      _context.Students.Remove(student);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
  }
}
