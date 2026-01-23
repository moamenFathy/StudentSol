using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Security;
using StudentSystem_DataAccess.Data;
using StudentSystem_Model.Models;
using StudentSystem_Model.ViewModel;

namespace StudentSystem.Controllers
{
  public class AccountController : Controller
  {
    private readonly ApplicationDbContext _context;
    public AccountController(ApplicationDbContext context)
    {
      _context = context;
    }

    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVm vm)
    {
      if (!ModelState.IsValid)
        return View(vm);

      var email = vm.Email.Trim().ToLower();

      var emailExists = await _context.Admins.AnyAsync(s => s.Email == email);

      if (emailExists)
      {
        ModelState.AddModelError(nameof(vm.Email), "Email already exists");
        return View(vm);
      }

      var (hash, salt) = PasswordHasher.HashPassword(vm.Password);

      var Admin = new EndUser
      {
        FirstName = vm.FirstName.Trim(),
        LastName = vm.LastName.Trim(),
        Email = email,
        Password = hash,
        PasswordSalt = salt,
      };

      await _context.Admins.AddAsync(Admin);
      await _context.SaveChangesAsync();

      return RedirectToAction(nameof(Login));
    }

    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginVm vm)
    {
      if (!ModelState.IsValid) return View(vm);

      var email = vm.Email.Trim().ToLower();

      var admin = _context.Admins.FirstOrDefault(s => s.Email == email);

      if (admin == null)
      {
        ModelState.AddModelError("", "Invalid email or password");
        return View(vm);
      }

      var isValidPassword = PasswordHasher.Verify(
          vm.Password,
          admin.Password,
          admin.PasswordSalt
        );

      if (!isValidPassword)
      {
        ModelState.AddModelError("", "Invalid email or password");
        return View(vm);
      }

      HttpContext.Session.SetInt32("adminId", admin.EndUserId);
      HttpContext.Session.SetString("adminName", admin.FullName);

      return RedirectToAction(nameof(Index), "Home");
    }

    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction(nameof(Index), "Home");
    }
  }
}
