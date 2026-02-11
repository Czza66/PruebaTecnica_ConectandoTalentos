using ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio;
using ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio.IRepositorio;
using ConectandoTalentosSolucion.Models;
using ConectandoTalentosSolucion.Models.ViewModels.Crud.Categoria;
using ConectandoTalentosSolucion.Utilidades;
using Humanizer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ConectandoTalentos.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ProtectorUtils _protectorUtils;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoriaController(IContenedorTrabajo contenedorTrabajo,
            ProtectorUtils protectorUtils,
            IWebHostEnvironment webHostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _protectorUtils = protectorUtils;
            _webHostEnvironment = webHostEnvironment;

        }

        [HttpGet]
        public IActionResult Index()
        {
            var categoria = _contenedorTrabajo.Categoria.GetAll()
                .OrderBy(c => c.orden)
                .Select(c => new CategoriaVM
                {
                    nombre = c.nombre,
                    descripcion = c.descripcion,
                    orden = c.orden,
                    activo = c.activo,
                    imgCategoria=c.imgCategoria,
                    CategoriaIdEncriptado = _protectorUtils.EncriptarInt(c.Id,"CategoriaTabla")
                });

            return View(categoria);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoriaCrearEditarVM vm) 
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var archivos = HttpContext.Request.Form.Files;
            if (archivos == null || archivos.Count == 0)
            {
                ModelState.AddModelError("", "Debe subir una imagen para la categoria.");
                return View(vm);
            }

            string rutaPrincipalArchivoCategoria = _webHostEnvironment.WebRootPath;
            var subidas = Path.Combine(rutaPrincipalArchivoCategoria, @"img\categoria");

            var extension = Path.GetExtension(archivos[0].FileName);
            string nombreArchivoCategoria = Guid.NewGuid().ToString();

            string rutaFinal = Path.Combine(subidas, nombreArchivoCategoria);
            using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivoCategoria + extension), FileMode.Create))
            {
                archivos[0].CopyTo(fileStreams);
            }

            var categoria = new Categoria
            {
                nombre=vm.nombre,
                descripcion=vm.descripcion,
                imgCategoria= @"\img\categoria\" + nombreArchivoCategoria + extension,
                orden =vm.orden,
                activo=true,
                fechaCreacion = DateTime.Now,
                fechaModificacion = DateTime.Now,
            };
            _contenedorTrabajo.Categoria.Add(categoria);
            _contenedorTrabajo.Save();
            TempData["RespuestaOperacion"] = "Categoria creada correctamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string id) 
        {
            var IdReal = _protectorUtils.DesencriptarInt(id, "CategoriaTabla");
            var categoria = _contenedorTrabajo.Categoria.GetFirstOrDefault(c => c.Id == IdReal);

            if (categoria == null)
            {
                TempData["RespuestaOperacion"] = "No pudiste acceder a ese item, intentalo mas tarde";
                return RedirectToAction(nameof(Index));
            }

            var categoriaEdit = new CategoriaCrearEditarVM
            {
                CategoriaId = _protectorUtils.EncriptarInt(IdReal, "CategoriaTabla"),
                nombre = categoria.nombre,
                descripcion = categoria.descripcion,
                orden = categoria.orden,
                activo = categoria.activo,
                imgCategoria = categoria.imgCategoria
            };
            return View(categoriaEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoriaCrearEditarVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var IdReal = _protectorUtils.DesencriptarInt(vm.CategoriaId, "CategoriaTabla");
            var categoria = _contenedorTrabajo.Categoria.GetFirstOrDefault(c => c.Id == IdReal);
            if (categoria == null)
            {
                TempData["RespuestaOperacion"] = "No pudiste acceder a ese item, intentalo mas tarde";
                return RedirectToAction(nameof(Index));
            }

            categoria.nombre = vm.nombre;
            categoria.descripcion = vm.descripcion;
            categoria.orden = vm.orden;
            categoria.activo = vm.activo;
            categoria.fechaModificacion = DateTime.Now;

            var archivos = HttpContext.Request.Form.Files;
            if (archivos != null && archivos.Count > 0)
            {
                if (!archivos[0].ContentType.StartsWith("image/"))
                {
                    ModelState.AddModelError("", "Debe subir un archivo de imagen válido (JPG/PNG).");
                    return View(vm);
                }

                string rutaPrincipalArchivoSlider = _webHostEnvironment.WebRootPath;
                var subidas = Path.Combine(rutaPrincipalArchivoSlider, @"img\categoria");

                var extension = Path.GetExtension(archivos[0].FileName);
                string nombreArchivoSlider = Guid.NewGuid().ToString();

                // Guardar nueva imagen
                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivoSlider + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }

                //eliminar imagen anterior
                if (!string.IsNullOrEmpty(categoria.imgCategoria))
                {
                    var rutaImagenAnterior = Path.Combine(rutaPrincipalArchivoSlider, categoria.imgCategoria.TrimStart('\\').Replace("/", "\\"));
                    if (System.IO.File.Exists(rutaImagenAnterior))
                    {
                        System.IO.File.Delete(rutaImagenAnterior);
                    }
                }
                // Guardar nueva ruta en BD
                categoria.imgCategoria = @"\img\categoria\" + nombreArchivoSlider + extension;
            }
            else
            {
                // Si NO subió imagen, conserva la actual
                categoria.imgCategoria = vm.imgCategoria;
            }
            _contenedorTrabajo.Categoria.Update(categoria);
            _contenedorTrabajo.Save();
            TempData["RespuestaOperacion"] = "Categoria actualizada correctamente";
            return RedirectToAction(nameof(Index));
        }

    }
}
