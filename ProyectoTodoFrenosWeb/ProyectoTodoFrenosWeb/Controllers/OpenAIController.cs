using Microsoft.AspNetCore.Mvc;
using OpenAI_API;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class OpenAIController : Controller
    {
        private readonly OpenAIAPI openAIAPI;
        public OpenAIController()
        {
            this.openAIAPI = new OpenAIAPI();
        }

        public IActionResult Index()
        {
            var questions = new List<string>
            {
                "¿Cuál es el horario de atención del taller?",
                "¿Qué tipos de pagos aceptan en el taller?",
                "¿Cuáles son los servicios que ofrecen?",
                "¿Cómo puedo programar una cita?",
                "¿Dónde está ubicado el taller?"
            };
            return View(questions);
        }

        [HttpPost]
        public async Task<IActionResult> GetAnswer(string question)
        {
            var predefinedAnswers = new Dictionary<string, string>
        {
            { "¿Cuál es el horario de atención del taller?", "El local está abiero al público de lunes a viernes 7:30 a.m. a 5:00 p.m. y sábado de 7:30 a.m. a 12:00 p.m." },
            { "¿Qué tipos de pagos aceptan en el taller?", "Aceptamos pago en efectivo, tarjeta, sinpe movil y transferencia bancaria" },
            { "¿Cuáles son los servicios que ofrecen?", "Nosotros ofrecemos: sistema de clutch, mecánica general, sistema de frenos, rescate en carretera 24hrs, mantenimiento, entre otros." },
            { "¿Cómo puedo programar una cita?", "En el sistema contamos con un apartado de agendar citas. Lo seleccionas, llenas el formulario y pronto estaremos validando tu cita." },
            { "¿Dónde está ubicado el taller?", "Estamos ubicados en 100 sur, 25 oeste de la entrada de emergencias Maternidad Carit." },
        };

            if (predefinedAnswers.ContainsKey(question))
            {
                return Json(new { answer = predefinedAnswers[question] });
            }
            else
            {
                return Json(new { answer = "Lo siento, no tengo una respuesta para esa pregunta." });
            }
        }
    }
}
