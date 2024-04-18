using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solucion.Data;
using Solucion.Models;

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
            return RedirectToAction("Home", "Employees");
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

    public async Task<IActionResult> ForgotPass(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPass(Employee employee)
    {
        if (ModelState.IsValid)
        {   
            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }
        return View(employee);
    }

    private bool EmployeeExists(int id)
    {
        return _context.Employees.Any(e => e.Id == id);
    }
}