using Microsoft.AspNetCore.Mvc;
using StudentSystem_DataAccess.Data;
using StudentSystem_Model.Models;

namespace StudentSystem.Controllers
{
  public class StudentController : Controller
  {
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context) => _context = context;

    public IActionResult Index()
    {
      List<Student> students = _context.Students.ToList();
      return View(students);
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
