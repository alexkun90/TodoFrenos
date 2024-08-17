using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class ManagePayrollsController : Controller
    {
        // GET: ManagePayrolls
        public ActionResult Index()
        {
            return View();
        }

        // GET: ManagePayrolls/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ManagePayrolls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagePayrolls/Create
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

        // GET: ManagePayrolls/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ManagePayrolls/Edit/5
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

        // GET: ManagePayrolls/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManagePayrolls/Delete/5
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
