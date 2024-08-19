using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTodoFrenosWeb.Controllers
{
    [Authorize(Roles = "Admin, Mecanico")]
    public class ManageAppointmentController : Controller
    {
        // GET: ManageAppointmentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ManageAppointmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ManageAppointmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageAppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageAppointmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ManageAppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageAppointmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManageAppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
