using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio.IRepositorio
{
    public interface IContenedorTrabajo :IDisposable
    {
        ICategoriaRepository Categoria { get; }
        IProductoRepository Producto { get; }

        void Save();
    }
}
