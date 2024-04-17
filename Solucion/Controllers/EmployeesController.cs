using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Employees");
    }

    
}