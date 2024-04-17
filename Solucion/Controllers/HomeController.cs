using Microsoft.AspNetCore.Mvc;
using Solucion.Models;
using Solucion.Data;
using Microsoft.EntityFrameworkCore;

namespace Solucion.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}