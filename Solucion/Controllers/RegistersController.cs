// using System.Collections.Generic;
// using Solucion.Data;
// using Solucion.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Http;

// namespace Solucion.Controllers
// {
//     public class RegistersController : Controller
//     {

//         public static DateTime Today { get; }
//         public BaseContext _context;
//         public RegistersController(BaseContext baseContext)
//         {

//             _context = baseContext;
//         }

//         /* public async Task<IActionResult> Index()
//         {
//             ViewBag.Names = HttpContext.Session.GetString("Names");
//             ViewBag.Entry = await _context.Registers.FirstOrDefaultAsync(r => r.EmployeeId == HttpContext.Session.GetInt32("Id") && r.Exits == null);
//             return View();
//         }  */
//         public async Task<IActionResult> Index()
//         {
//             // Leer el nombre del empleado desde la cookie
//             ViewBag.Names = HttpContext.Request.Cookies["Name"];

//             // Leer el ID del empleado desde la cookie
//             var EmployeeId = HttpContext.Request.Cookies["EmployeeId"];

//             // Verificar si la cookie de ID de empleado existe y si es válido
//             if (string.IsNullOrEmpty(EmployeeId))
//             {
//                 // Manejar el caso en que la cookie no existe o el ID del empleado no es válido
//                 // Por ejemplo, redirigir al usuario a una página de inicio de sesión
//                 return RedirectToAction("Main", "Employees");
//             }

//         //     // Buscar si existe algún registro de entrada para el empleado en la base de datos
//         //     ViewBag.Entry = await _context.FirstOrDefaultAsync(r => r.EmployeeId == Convert.ToInt32(EmployeeId) && r.Exits == null);
            
//         //     // Retornar la vista
//         //     return View();
//         // }

//   /*       public async Task<IActionResult> CheckIn()
//         {
//             try
//             {
//                 var EmployeeId = HttpContext.Session.GetInt32("Id");

//                 var register = new Register()
//                 {
//                     Entries = DateTime.Now,
//                     EmployeeId = EmployeeId
//                 };
//                 _context.Registers.Add(register);
//                 await _context.SaveChangesAsync();
//                 TempData["MessageSuccess"] = "Se ha generado el registro de entrada correctamente";
//                 return RedirectToAction("Index");
//             }
//             catch (System.Exception)
//             {
//                 // aqui mensaje a slack mediante webhook
//                 throw;
//             }
//         } */


//         public async Task<IActionResult> CheckIn()
//         {
//             try
//             {
//                 // Leer el ID del empleado desde la cookie
//                 var EmployeeId = HttpContext.Request.Cookies["EmployeeId"];

//                 // Verificar si la cookie existe y si el ID del empleado es válido
//                 if (string.IsNullOrEmpty(EmployeeId))
//                 {
//                     // Manejar el caso en que la cookie no existe o el ID del empleado no es válido
//                     // Por ejemplo, redirigir al usuario a una página de inicio de sesión
//                     return RedirectToAction("Home");
//                 }

//                 // Crear un nuevo registro con la fecha y hora actual y el ID del empleado
//                 var register = new Register()
//                 {
//                     Entries = DateTime.Now,
//                     EmployeeId = Convert.ToInt32(EmployeeId) // Convertir el ID del empleado a entero si es necesario
//                 };

//                 // Agregar el registro a la base de datos y guardar los cambios
//                 _context.Registers.Add(register);
//                 await _context.SaveChangesAsync();

//                 // Establecer un mensaje de éxito para mostrar al usuario
//                 TempData["MessageSuccess"] = "Se ha generado el registro de entrada correctamente";

//                 // Redirigir al usuario a la página de inicio o a donde sea apropiado
//                 return RedirectToAction("Home");
//             }
//             catch (System.Exception)
//             {
//                 // Manejar excepciones aquí, por ejemplo, enviar un mensaje a un servicio de notificación como Slack
//                 throw;
//             }
//         }

//         /* public async Task<IActionResult> CheckOut()
//         {
//             try
//             {
//                 var CheckOut = await _context.Registers.FirstOrDefaultAsync(r => r.EmployeeId == HttpContext.Session.GetInt32("Id") && r.Exits == null);
//                 CheckOut.Exits = DateTime.Now;
//                 await _context.SaveChangesAsync();
//                 TempData["MessageSuccess"] = "Se ha generado el registro de tu salida correctamente";
//                 return RedirectToAction("Index");

//             }
//             catch (System.Exception)
//             {
//                 // aqui mensaje a slack mediante webhook
//                 throw;
//             }
//         } */

//     }
// }