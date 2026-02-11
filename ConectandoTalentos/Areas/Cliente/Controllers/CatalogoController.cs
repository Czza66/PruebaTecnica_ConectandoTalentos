using ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio.IRepositorio;
using ConectandoTalentosSolucion.Models.ViewModels.CatalogoVM;
using Microsoft.AspNetCore.Mvc;

namespace ConectandoTalentos.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class CatalogoController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CatalogoController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index(List<int>? categorias)
        {
            // si viene una categoría desde Home, ya viene aquí
            categorias ??= new List<int>();

            var categoriasActivas = _contenedorTrabajo.Categoria.GetAll(
                filter: c => c.activo,
                orderbBy: q => q.OrderBy(c => c.orden)
            ).ToList();

            var productosQuery = _contenedorTrabajo.Producto.GetAll(
                filter: p => p.activo
            );

            // 👉 SI hay categorías seleccionadas, filtra
            if (categorias.Any())
            {
                productosQuery = productosQuery
                    .Where(p => categorias.Contains(p.CategoriaId));
            }

            var productos = productosQuery
                .Select(p => new ProductoCardVM
                {
                    Id = p.Id,
                    Nombre = p.nombre,
                    Precio = p.valor,
                    Cantidad = p.cantidad,
                    ImagenUrl = p.imgProducto,
                    CategoriaId = p.CategoriaId
                })
                .ToList();

            var vm = new CatalogoVM
            {
                CategoriasSeleccionadas = categorias,

                Categorias = categoriasActivas.Select(c => new CategoriaFiltroVM
                {
                    Id = c.Id,
                    Nombre = c.nombre,
                    Seleccionada = categorias.Contains(c.Id) // 🔥 AQUÍ SE MARCA
                }).ToList(),

                Productos = productos
            };

            return View(vm);
        }
    }
}
