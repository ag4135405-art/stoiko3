using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stoiko3.Controllers
{
    public class DivisionMuscularController : Controller
    {
        // GET: DivisionMuscularController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DivisionMuscularController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DivisionMuscularController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DivisionMuscularController/Create
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

        // GET: DivisionMuscularController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DivisionMuscularController/Edit/5
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

        // GET: DivisionMuscularController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DivisionMuscularController/Delete/5
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
