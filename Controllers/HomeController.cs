using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stoiko3.Models;
using Supabase;
using System.Diagnostics;

namespace stoiko3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Client _supabaseClient;

        public HomeController(ILogger<HomeController> logger, Client supabaseClient)
        {
            _logger = logger;
            _supabaseClient = supabaseClient;
        }

        // ==========================================
        // PÁGINA DE BIENVENIDA (INDEX)
        // ==========================================
        public IActionResult Index()
        {
            return View();
        }

        // ==========================================
        // VISTA DE INICIO DE SESIÓN (iniciar_ses.cshtml)
        // ==========================================
        [HttpGet]
        public IActionResult iniciar_ses()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> iniciar_ses(string email, string password, string captchaInput, string ExpectedCaptcha)
        {
            string cleanEmail = email?.Trim() ?? string.Empty;
            string cleanPassword = password ?? string.Empty;
            string cleanCaptchaInput = captchaInput?.Trim() ?? string.Empty;
            string cleanExpectedCaptcha = ExpectedCaptcha?.Trim() ?? string.Empty;

            _logger.LogInformation($"[SUPABASE LOGIN] Email: '{cleanEmail}'");

            // 1. Validar Captcha
            if (string.IsNullOrEmpty(cleanCaptchaInput) || !cleanCaptchaInput.Equals(cleanExpectedCaptcha, StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.ErrorMessage = "El código Captcha es incorrecto. Inténtalo de nuevo.";
                return View();
            }

            // 2. Buscar al usuario directamente en Supabase
            Usuario? user = null;
            try
            {
                var response = await _supabaseClient
                    .From<Usuario>()
                    .Where(u => u.EmailUsu == cleanEmail)
                    .Get();

                user = response.Models.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ERROR SUPABASE] Fallo de conexión: {ex.Message}");
                ViewBag.ErrorMessage = $"Error al conectar con la base de datos: {ex.Message}";
                return View();
            }

            // 3. Validar si el usuario existe
            if (user == null)
            {
                _logger.LogWarning($"[LOGIN FALLO] El correo '{cleanEmail}' no existe en Supabase.");
                ViewBag.ErrorMessage = $"El correo '{cleanEmail}' no se encuentra registrado en el sistema.";
                return View();
            }

            // 4. Validar contraseña con BCrypt contra pass_usu
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(cleanPassword, user.PassUsu);
            if (!isPasswordCorrect)
            {
                _logger.LogWarning($"[LOGIN FALLO] Contraseña incorrecta para '{cleanEmail}'.");
                ViewBag.ErrorMessage = "La contraseña ingresada es incorrecta.";
                return View();
            }

            // 5. Actualizar última fecha de inicio de sesión
            try
            {
                user.FechaInicioSecion = DateTime.UtcNow;
                await user.Update<Usuario>();
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"No se pudo actualizar fecha_iniciosecion: {ex.Message}");
            }

            // 6. Iniciar sesión y redirigir
            _logger.LogInformation($"[LOGIN ÉXITO] Redirigiendo a perfil_usu para '{cleanEmail}'...");
            HttpContext.Session.SetString("UserEmail", cleanEmail);
            HttpContext.Session.SetInt32("UserId", user.IdUsu);

            return RedirectToAction("perfil_usu");
        }

        // ==========================================
        // PANEL PRINCIPAL / PERFIL DE USUARIO (perfil_usu.cshtml)
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> perfil_usu()
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("iniciar_ses");
            }

            // Obtener información del perfil desde Supabase
            try
            {
                var userResponse = await _supabaseClient
                    .From<Usuario>()
                    .Where(u => u.EmailUsu == userEmail)
                    .Get();

                var user = userResponse.Models.FirstOrDefault();
                if (user != null)
                {
                    var perfilResponse = await _supabaseClient
                        .From<PerfilUsu>()
                        .Where(p => p.IdUsu == user.IdUsu)
                        .Get();

                    ViewBag.Perfil = perfilResponse.Models.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"No se pudo cargar la información detallada del perfil: {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult perfil_usu(IFormCollection collection)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("iniciar_ses");
            }

            return View();
        }

        // ==========================================
        // REGISTRO (CREAR CUENTA EN SUPABASE + PERFIL INICIAL)
        // ==========================================
        [HttpGet]
        public IActionResult registrousuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> registrousuario(string Email, string Password, string ConfirmPassword, string CaptchaInput, string ExpectedCaptcha)
        {
            string cleanEmail = Email?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(CaptchaInput) || !CaptchaInput.Trim().Equals(ExpectedCaptcha?.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.ErrorMessage = "El código Captcha es incorrecto.";
                return View();
            }

            if (Password != ConfirmPassword)
            {
                ViewBag.ErrorMessage = "Las contraseñas no coinciden.";
                return View();
            }

            try
            {
                // 1. Verificar si ya existe en Supabase
                var existingUserResponse = await _supabaseClient
                    .From<Usuario>()
                    .Where(u => u.EmailUsu == cleanEmail)
                    .Get();

                if (existingUserResponse.Models.Any())
                {
                    ViewBag.ErrorMessage = "Este correo electrónico ya está registrado.";
                    return View();
                }

                // 2. Generar Hash y Guardar Usuario
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(Password);

                var nuevoUsuario = new Usuario
                {
                    EmailUsu = cleanEmail,
                    PassUsu = passwordHash,
                    FechaRegistro = DateTime.UtcNow
                };

                var insertUserResult = await _supabaseClient.From<Usuario>().Insert(nuevoUsuario);
                var usuarioCreado = insertUserResult.Models.FirstOrDefault();

                // 3. Crear Perfil asociado automáticamente
                if (usuarioCreado != null)
                {
                    var nuevoPerfil = new PerfilUsu
                    {
                        IdUsu = usuarioCreado.IdUsu,
                        NomPerfil = cleanEmail.Split('@')[0],
                        ScoreTotal = 0,
                        NumeroRachas = 0
                    };

                    await _supabaseClient.From<PerfilUsu>().Insert(nuevoPerfil);
                }

                _logger.LogInformation($"[REGISTRO ÉXITO] Usuario y Perfil para '{cleanEmail}' creados en Supabase.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[REGISTRO ERROR] {ex.Message}");
                ViewBag.ErrorMessage = $"Error al registrar en Supabase: {ex.Message}";
                return View();
            }

            return RedirectToAction("iniciar_ses");
        }

        // ==========================================
        // ENDPOINT DE PRUEBA RÁPIDA DE CONEXIÓN
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> TestSupabase()
        {
            try
            {
                var response = await _supabaseClient.From<Usuario>().Get();
                int totalUsuarios = response.Models.Count;
                return Content($"¡CONEXIÓN EXITOSA CON SUPABASE! Se encontraron {totalUsuarios} usuarios registrados.");
            }
            catch (Exception ex)
            {
                return Content($"ERROR DE CONEXIÓN CON SUPABASE: {ex.Message}");
            }
        }

        // ==========================================
        // CERRAR SESIÓN
        // ==========================================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("iniciar_ses");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}