using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stoiko3.Models;
using Supabase.Postgrest;

namespace stoiko3.Controllers
{
    public class DivisionGeneralController : Controller
    {
        private readonly Supabase.Client _supabase;

        public DivisionGeneralController(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        
        // GET: DivisionGeneralController
        public async Task<IActionResult> Index()
        {
            var response = await _supabase.From<DivisionGeneral>().Get();
            var divisionGenerals = response.Models;
            return View(divisionGenerals);
        }

        // GET: DivisionGeneralController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _supabase.From<DivisionGeneral>()
                .Where(d => d.IdDivision == id)
                .Get();

            var division = response.Models.FirstOrDefault();
            if (division == null) return NotFound();

            return View(division);
        }

        // GET: DivisionGeneralController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DivisionGeneralController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DivisionGeneral divisionGeneral)
        {
            if (!ModelState.IsValid) return View(divisionGeneral);

            await _supabase.From<DivisionGeneral>().Insert(divisionGeneral);
            return RedirectToAction(nameof(Index));
        }

        // GET: DivisionGeneralController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _supabase.From<DivisionGeneral>()
                .Where(d => d.IdDivision == id)
                .Get();

            var division = response.Models.FirstOrDefault();
            if (division == null) return NotFound();

            return View(division);
        }

        // POST: DivisionGeneralController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DivisionGeneral divisionGeneral)
        {
            if (id != divisionGeneral.IdDivision) return BadRequest();
            if (!ModelState.IsValid) return View(divisionGeneral);

            await _supabase.From<DivisionGeneral>()
                .Where(d => d.IdDivision == id)
                .Update(divisionGeneral);

            return RedirectToAction(nameof(Index));
        }

        // GET: DivisionGeneralController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _supabase.From<DivisionGeneral>()
                .Where(d => d.IdDivision == id)
                .Get();

            var division = response.Models.FirstOrDefault();
            if (division == null) return NotFound();

            return View(division);
        }

        // POST: DivisionGeneralController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DivisionGeneral divisionGeneral)
        {
            await _supabase.From<DivisionGeneral>()
                .Where(d => d.IdDivision == id)
                .Delete();

            return RedirectToAction(nameof(Index));
        }
    }
}
