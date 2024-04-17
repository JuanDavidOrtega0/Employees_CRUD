using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solucion.Models;
using Solucion.Data;

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

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
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
}