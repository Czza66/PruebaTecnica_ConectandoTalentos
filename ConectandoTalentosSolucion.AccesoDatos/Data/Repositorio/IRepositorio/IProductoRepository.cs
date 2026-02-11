using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConectandoTalentosSolucion.Models;

namespace ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio.IRepositorio
{
    public interface IProductoRepository : IRepository<Productos>
    {
        void Update(Productos productos);
    }
}
