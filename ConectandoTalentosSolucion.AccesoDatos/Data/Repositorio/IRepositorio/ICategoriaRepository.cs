using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConectandoTalentosSolucion.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio.IRepositorio
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        void Update(Categoria categoria);
        IEnumerable<SelectListItem> GetListaCategorias();
    }
}
