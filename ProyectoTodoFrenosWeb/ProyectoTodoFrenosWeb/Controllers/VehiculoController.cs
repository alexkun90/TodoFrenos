using Microsoft.AspNetCore.Mvc;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class VehiculoController : Controller
    {
        [HttpGet]
        public IActionResult ListaVehiculos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminVehiculos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AgregarVehiculos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DetalleVehiculos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ModificarVehiculos()
        {
            return View();
        }
    }
}
