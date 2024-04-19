using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solucion.Data;
using Solucion.Models;
using Microsoft.AspNetCore.Identity;

namespace Solucion.Controllers;

public class EmployeesController : Controller
{
    private readonly BaseContext _context;

    public EmployeesController(BaseContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(u => u.Email == email);
        if (employee != null && employee.Password == password)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.Email),
            };

            var employeesIdentity = new ClaimsIdentity(claims, "login");

            var main = new ClaimsPrincipal(employeesIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, main);
            if (employee.Role == "Administrador")
            {
                return RedirectToAction("Home", "Employees");
            }
            else
            {
                return RedirectToAction("Main", "Employees");
            }
        }
        ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos");
        return View("Index");
    }

    [Authorize]
    public async Task<IActionResult> Home()
    {
        var employee = await _context.Employees.ToListAsync();
        return View(employee);
    }

    [Authorize]
    public async Task<IActionResult> Main()
    {
        var employee = await _context.Employees.ToListAsync();
        return View(employee);
    }

    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(employee);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        Response.Cookies.Append("LoggedOut", "true");
        return RedirectToAction("Index", "Employees");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return RedirectToAction("Home");
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    public async Task<IActionResult> DetailsMain(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    public IActionResult ForgotPass()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPass(string email, string newPassword, string confirmPassword)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Email == email);
        if (employee != null && employee.Email == email)
        {
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Las contraseñas no coinciden");
                return View("ForgotPass");
            }
            employee.Password = newPassword;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        else 
        {
            ModelState.AddModelError(string.Empty, "Correo no registrado");
            return View("ForgotPass");
        }
    }

    [Authorize]
    public IActionResult Search(string searchString)
    {
        var employees = _context.Employees.AsQueryable();
        if (!string.IsNullOrEmpty(searchString))
        {
            employees = employees.Where(x => x.Name.Contains(searchString) || x.LastName.Contains(searchString) || x.Email.Contains(searchString));
        }
        return View("Home", employees.ToList());
    }

    [Authorize]
    public IActionResult SearchMain(string searchString)
    {
        var employees = _context.Employees.AsQueryable();
        if (!string.IsNullOrEmpty(searchString))
        {
            employees = employees.Where(x => x.Name.Contains(searchString) || x.LastName.Contains(searchString) || x.Email.Contains(searchString));
        }
        return View("Main", employees.ToList());
    }

    
}