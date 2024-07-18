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
                "¿Cuál es tu horario?",
                "¿Dónde están ubicados?"
            };
            return View(questions);
        }

        [HttpPost]
        public async Task<IActionResult> GetAnswer(string question)
        {
            var predefinedAnswers = new Dictionary<string, string>
        {
            { "¿Cuál es tu horario?", "Nuestro horario es de 9am a 6pm de lunes a viernes." },
            { "¿Dónde están ubicados?", "Estamos ubicados en la Calle Falsa 123, Springfield." },
            // Añade más preguntas y respuestas predefinidas aquí
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
