using System.Diagnostics;
using ConectandoTalentos.Models;
using ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio.IRepositorio;
using ConectandoTalentosSolucion.Models.ViewModels.HomeVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConectandoTalentos.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HomeController(ILogger<HomeController> logger,
            IContenedorTrabajo contenedorTrabajo)
        {
            _logger = logger;
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            ViewData["FullWidth"] = true;

            var categoriasQuery = _contenedorTrabajo.Categoria.GetAll(
                filter: c => c.activo,
                orderbBy: q => q.OrderBy(c => c.orden)
                );

            var categorias = categoriasQuery
                .Select(c => new CategoriaCardVM
                   {
                       Id = c.Id,
                       Nombre = c.nombre,
                       Descripcion = c.descripcion,
                       ImgCategoria = c.imgCategoria,
                       Orden = c.orden
                   })
                .ToList();

            var vm = new HomeIndexVM
            {
                Categorias = categorias
            };

            return View(vm);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
