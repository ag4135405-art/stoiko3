using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stoiko3.Models;
using System.Diagnostics;

namespace stoiko3.Controllers
{
    public class PerfilUsuController : Controller
    {
        // ==========================================
        // PANEL PRINCIPAL DE PERFIL
        // ==========================================
        [HttpGet]
        public IActionResult Index()
        {
            // Validar que el usuario haya iniciado sesión
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("iniciar_ses", "Home");
            }

            return View(); // Buscará la vista en Views/PerfilUsu/Index.cshtml
        }

        // Si prefieres que la vista se llame directamente "perfil_usu.cshtml" dentro de esta carpeta:
        [HttpGet]
        public IActionResult perfil_usu()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("iniciar_ses", "Home");
            }

            return View(); // Buscará Views/PerfilUsu/perfil_usu.cshtml
        }

        // Métodos auxiliares por si mantienes el CRUD básico del scaffolding:
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

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
    }
}