using System.Security.Claims;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solucion.Data;
using Solucion.Models;

namespace Solucion.Controllers
{
    public class RecordsController : Controller
    {

        private readonly BaseContext _context;

        public RecordsController(BaseContext baseContext)
        {
            _context = baseContext;
        }

        /* [Authorize]
        public async Task<IActionResult> Index()
        {
            var record = await _context.Records.ToListAsync();
            return View(record);
        } */

        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Obtener valores de las cookies
            var nameCookie = HttpContext.Request.Cookies["Name"];
            var idCookie = HttpContext.Request.Cookies["Employee_Id"];
            Console.WriteLine(idCookie);
            // Establecer valores predeterminados en caso de que las cookies estén ausentes
            ViewBag.Name = nameCookie; // Puedes establecer un valor predeterminado o dejarlo como nulo si prefieres
            ViewBag.Entry = await _context.Records.FirstOrDefaultAsync(r => r.Employee_Id == Convert.ToInt32(idCookie) && r.RegisterExit == null);
            var employee = await _context.Records.ToListAsync();

            return View(employee);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CheckIn()
        {
            try
            {
                var EmployeeId = HttpContext.Request.Cookies["Employee_Id"];
                var record = new Record()
                {
                    RegisterEntry = DateTime.Now,
                    Employee_Id = Convert.ToInt32(EmployeeId)
                };

                await _context.Records.AddAsync(record);
                await _context.SaveChangesAsync();

                TempData["MessageSuccess"] = "Se ha generado el registro de entrada correctamente";
                return RedirectToAction("History");
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        public async Task<IActionResult> CheckOut()
        {
            try
            {
                var idCookie = HttpContext.Request.Cookies["Employee_Id"];
                var CheckOut = await _context.Records.FirstOrDefaultAsync(r => r.Employee_Id == Convert.ToInt32(idCookie) && r.RegisterExit == null);
                CheckOut.RegisterExit = DateTime.Now;

                await _context.SaveChangesAsync();

                TempData["MessageSuccess"] = "Se ha generado el registro de tu salida correctamente";
                return RedirectToAction("History");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        public IActionResult SearchHistory(string searchString)
        {
            var history = _context.Records.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                history = history.Where(x => x.RegisterEntry.ToString().Contains(searchString) || x.RegisterExit.ToString().Contains(searchString) || x.Employee_Id.ToString().Contains(searchString));
            }
            return View("Index", history.ToList());
        }

        public async Task<IActionResult> DeleteHistoryView(int? id)
        {
            var history = await _context.Records.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }
            return View(history);
        }

        public async Task<IActionResult> DeleteHistory(int id)
        {
            var history = await _context.Records.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }
            _context.Records.Remove(history);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteRecordView(int? id)
        {
            var history = await _context.Records.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }
            return View(history);
        }

        public async Task<IActionResult> DeleteRecord(int id)
        {
            var history = await _context.Records.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }
            _context.Records.Remove(history);
            await _context.SaveChangesAsync();
            return RedirectToAction("History");
        }

        [Authorize]
        public async Task<IActionResult> History()
        {   
            var nameCookie = HttpContext.Request.Cookies["Name"];
            var idCookie = HttpContext.Request.Cookies["Employee_Id"];
            var employee = await _context.Records.ToListAsync();
            ViewBag.Entry = await _context.Records.FirstOrDefaultAsync(r => r.Employee_Id == Convert.ToInt32(idCookie) && r.RegisterExit == null);
            return View(employee);
        }

        [Authorize]
        public IActionResult SearchRecord(string searchString)
        {
            var history = _context.Records.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                history = history.Where(x => x.RegisterEntry.ToString().Contains(searchString) || x.RegisterExit.ToString().Contains(searchString) || x.Employee_Id.ToString().Contains(searchString));
            }
            return View("History", history.ToList());
        }
    }
}