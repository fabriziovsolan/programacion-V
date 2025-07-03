using Microsoft.AspNetCore.Mvc;
using MvcMovie1.Models;

namespace MvcMovie1.Controllers
{
    public class PrincipalController : Controller
    {
        public IActionResult Principal()
        {
            var modelo = new PrincipalModel
            {
                Mensaje = "Bienvenidos al Panel Principal",
                FechaIngreso = DateTime.Now
            };

            ViewBag.Saludo = "Este saludo viene desde ViewBag también.";

            return View("Principal", modelo);

        }
    }
}
