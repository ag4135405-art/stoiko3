using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stoiko3.Controllers
{
    public class MusculoController : Controller
    {
        // GET: MusculoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MusculoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MusculoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MusculoController/Create
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

        // GET: MusculoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MusculoController/Edit/5
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

        // GET: MusculoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MusculoController/Delete/5
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
