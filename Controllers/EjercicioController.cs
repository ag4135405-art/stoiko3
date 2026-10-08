using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stoiko3.Controllers
{
    public class EjercicioController : Controller
    {
        // GET: EjercicioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EjercicioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EjercicioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EjercicioController/Create
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

        // GET: EjercicioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EjercicioController/Edit/5
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

        // GET: EjercicioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EjercicioController/Delete/5
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
