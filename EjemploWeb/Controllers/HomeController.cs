using EjemploWeb.Data;
using EjemploWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EjemploWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private PersonasConsumoApi personasConsumoApi;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            personasConsumoApi = new PersonasConsumoApi();
        }

        public async Task<IActionResult> Index()
        {
            List<Persona> listadoPersonas = await  personasConsumoApi.BuscarTodos();
            ViewBag.listadoPersonas = listadoPersonas;

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> BuscarUno(int id)
        {
            Persona personaEncontrada = await personasConsumoApi.BuscarUno(id);
            ViewBag.personaEncontrada = personaEncontrada;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(Persona persona)
        {
          var result =  await personasConsumoApi.GuardarPersona(persona);
          return View(result);

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