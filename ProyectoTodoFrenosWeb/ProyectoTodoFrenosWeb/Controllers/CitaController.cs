using Microsoft.AspNetCore.Mvc;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class CitaController : Controller
    {
        [HttpGet]
        public IActionResult SolicitudCita()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminCitas()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EvaluarCitas()
        {
            return View();
        }
    }
}
