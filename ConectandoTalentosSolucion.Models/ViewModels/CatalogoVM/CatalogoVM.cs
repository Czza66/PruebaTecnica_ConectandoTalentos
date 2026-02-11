using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectandoTalentosSolucion.Models.ViewModels.CatalogoVM
{
    public class CatalogoVM
    {
        public List<CategoriaFiltroVM> Categorias { get; set; } = new();
        public List<ProductoCardVM> Productos { get; set; } = new();

        // ids seleccionados (checkbox)
        public List<int> CategoriasSeleccionadas { get; set; } = new();
    }

    public class CategoriaFiltroVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public bool Seleccionada { get; set; }
    }

    public class ProductoCardVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string? ImagenUrl { get; set; }
        public int CategoriaId { get; set; }
    }
}
