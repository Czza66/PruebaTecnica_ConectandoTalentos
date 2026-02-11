using System.Collections.Immutable;
using ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio.IRepositorio;
using ConectandoTalentosSolucion.Models.ViewModels.Crud.Categoria;
using ConectandoTalentosSolucion.Models;
using ConectandoTalentosSolucion.Models.ViewModels.Crud.Producto;
using ConectandoTalentosSolucion.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace ConectandoTalentos.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductoController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ProtectorUtils _protectorUtils;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoController(IContenedorTrabajo contenedorTrabajo,
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
            var producto = _contenedorTrabajo.Producto.GetAll()
                .OrderBy(p => p.orden)
                .Select(p => new ProductoVM
                {
                    Categoria = p.categoria.nombre,
                    nombre = p.nombre,
                    descripcion = p.descripcion,
                    imgProducto = p.imgProducto,
                    cantidad = p.cantidad,
                    valor = p.valor,
                    orden = p.orden,
                    activo = p.activo,
                    IdProductoEncriptado = _protectorUtils.EncriptarInt(p.Id, "ProductoTabla")

                });

            return View(producto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ProductoCrearEditarVM
            {
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductoCrearEditarVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();
                return View(vm);
            }

            var archivos = HttpContext.Request.Form.Files;
            if (archivos == null || archivos.Count == 0)
            {
                ModelState.AddModelError("", "Debe subir una imagen para la categoria.");
                return View(vm);
            }

            string rutaPrincipalArchivoCategoria = _webHostEnvironment.WebRootPath;
            var subidas = Path.Combine(rutaPrincipalArchivoCategoria, @"img\producto");

            var extension = Path.GetExtension(archivos[0].FileName);
            string nombreArchivoCategoria = Guid.NewGuid().ToString();

            string rutaFinal = Path.Combine(subidas, nombreArchivoCategoria);
            using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivoCategoria + extension), FileMode.Create))
            {
                archivos[0].CopyTo(fileStreams);
            }

            var prodcuto = new Productos
            {
                nombre = vm.nombre,
                descripcion = vm.descripcion,
                imgProducto = @"\img\producto\" + nombreArchivoCategoria + extension,
                cantidad = vm.cantidad,
                valor = vm.valor,
                CategoriaId = vm.CategoriaId,
                orden = vm.orden,
                activo = true,
                fechaCreacion = DateTime.Now,
                fechaModificacion = DateTime.Now,
            };
            _contenedorTrabajo.Producto.Add(prodcuto);
            _contenedorTrabajo.Save();
            TempData["RespuestaOperacion"] = "Producto creado correctamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var IdReal = _protectorUtils.DesencriptarInt(id, "ProductoTabla");
            var producto = _contenedorTrabajo.Producto.GetFirstOrDefault(p => p.Id == IdReal);

            if (producto == null)
            {
                TempData["RespuestaOperacion"] = "No pudiste acceder a ese item, intentalo mas tarde";
                return RedirectToAction(nameof(Index));
            }

            var productoEdit = new ProductoCrearEditarVM
            {
                IdProducto = _protectorUtils.EncriptarInt(IdReal, "ProductoTabla"),
                nombre = producto.nombre,
                descripcion = producto.descripcion,
                cantidad = producto.cantidad,
                valor = producto.valor,
                CategoriaId = producto.CategoriaId,
                orden = producto.orden,
                activo = producto.activo,
                imgProducto = producto.imgProducto,
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias(),
            };
            return View(productoEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductoCrearEditarVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var IdReal = _protectorUtils.DesencriptarInt(vm.IdProducto, "ProductoTabla");
            var producto = _contenedorTrabajo.Producto.GetFirstOrDefault(c => c.Id == IdReal);
            if (producto == null)
            {
                TempData["RespuestaOperacion"] = "No pudiste acceder a ese item, intentalo mas tarde";
                return RedirectToAction(nameof(Index));
            }

            producto.nombre = vm.nombre;
            producto.descripcion = vm.descripcion;
            producto.cantidad = vm.cantidad;
            producto.valor = vm.valor;
            producto.CategoriaId = vm.CategoriaId;
            producto.orden = vm.orden;
            producto.activo = vm.activo;
            producto.fechaModificacion = DateTime.Now;

            var archivos = HttpContext.Request.Form.Files;
            if (archivos != null && archivos.Count > 0)
            {
                if (!archivos[0].ContentType.StartsWith("image/"))
                {
                    ModelState.AddModelError("", "Debe subir un archivo de imagen válido (JPG/PNG).");
                    return View(vm);
                }

                string rutaPrincipalArchivoProdcutos = _webHostEnvironment.WebRootPath;
                var subidas = Path.Combine(rutaPrincipalArchivoProdcutos, @"img\producto");

                var extension = Path.GetExtension(archivos[0].FileName);
                string nombreArchivoProductos = Guid.NewGuid().ToString();

                // Guardar nueva imagen
                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivoProductos + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }

                //eliminar imagen anterior
                if (!string.IsNullOrEmpty(producto.imgProducto))
                {
                    var rutaImagenAnterior = Path.Combine(rutaPrincipalArchivoProdcutos, producto.imgProducto.TrimStart('\\').Replace("/", "\\"));
                    if (System.IO.File.Exists(rutaImagenAnterior))
                    {
                        System.IO.File.Delete(rutaImagenAnterior);
                    }
                }
                // Guardar nueva ruta en BD
                producto.imgProducto = @"\img\producto\" + nombreArchivoProductos + extension;
            }
            else
            {
                // Si NO subió imagen, conserva la actual
                producto.imgProducto = vm.imgProducto;
            }
            _contenedorTrabajo.Producto.Update(producto);
            _contenedorTrabajo.Save();
            TempData["RespuestaOperacion"] = "Producto actualizado correctamente";
            return RedirectToAction(nameof(Index));
        }

    }
}
