using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectandoTalentosSolucion.Models.ViewModels.HomeVM
{
    public class HomeIndexVM
    {
        public List<CategoriaCardVM> Categorias { get; set; } = new();
    }

    public class CategoriaCardVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string ImgCategoria { get; set; } = "";
        public int Orden { get; set; }
    }
}
