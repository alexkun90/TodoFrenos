using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

/*namespace ProyectoTodoFrenosWeb.Controllers
{
    public class EjemploController : Controller
    { 
     private readonly Caso2DBContext _context;
    ServicesProfesor profesorServices;

    public ProfesorController(Caso2DBContext context, IConfiguration config)
    {
        _context = context;
        profesorServices = new ServicesProfesor(config);
    }

    // GET: Profesor
    public async Task<IActionResult> Index()
    {
        var profesores = await profesorServices.GetProfesor();

        return View(profesores);
    }

    // GET: Profesor/Details/5
    [Route("/Profesores/Detalles/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        Profesor profesor = await profesorServices.GetProfesor(id);

        if (id == null)
        {
            return NotFound();
        }

        return View(profesor);
    }

    // GET: Profesor/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Profesor/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Profesor profesor)
    {
        if (ModelState.IsValid)
        {


            // Configurar la fecha de creación
            profesor.FechaCreacion = DateTime.Now;

            var resultado = await profesorServices.CreateProfesor(profesor);

            if (resultado != null)
            {
                TempData["MenasajeExito"] = "Se guardo el registro exitosamente";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(profesor);
            }
        }
        return View(profesor);
    }

    // GET: Profesor/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Profesor profesor = await profesorServices.GetProfesor(id);

        if (profesor == null)
        {
            return NotFound();
        }
        return View(profesor);
    }

    // POST: Profesor/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Profesor profesor)
    {
        if (id != profesor.ProfesorId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {

                var resultado = await profesorServices.EditProfesor((int)id, profesor);

                if (resultado != null)
                {
                    TempData["MenasajeExito"] = "Se guardo el registro exitosamente";
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(profesor.ProfesorId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


        }
        return View(profesor);
    }

    // GET: Profesor/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Profesor profesor = await profesorServices.GetProfesor(id);

        if (profesor == null)
        {
            return NotFound();
        }

        return View(profesor);
    }

    // POST: Profesor/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var resultado = await profesorServices.DeleteProfesor((int)id);

        if (resultado != null)
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Delete));
    }

    private bool ProfesorExists(int? id)
    {
        return _context.Profesores.Any(e => e.ProfesorId == id);
    }
}
}
*/