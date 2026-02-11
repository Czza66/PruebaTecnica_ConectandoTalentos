using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio.IRepositorio;
using ConectandoTalentosSolucion.Models;
using ConectandoTalentosSolucion.Models.ViewModels.Crud.Categoria;

namespace ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio
{
    public class ProductoRepository : Repository<Productos>, IProductoRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductoRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Productos productos)
        {
            var objDesdeDB = _db.productos.FirstOrDefault(p => p.Id == productos.Id);
            if (objDesdeDB != null)
            {
                objDesdeDB.nombre = productos.nombre;
                objDesdeDB.cantidad = productos.cantidad;
                objDesdeDB.valor = productos.valor;
                objDesdeDB.fechaModificacion = productos.fechaModificacion;
                objDesdeDB.CategoriaId = productos.CategoriaId;
                objDesdeDB.orden = productos.orden;
                objDesdeDB.activo = productos.activo;
                objDesdeDB.descripcion = productos.descripcion;
                objDesdeDB.imgProducto = productos.imgProducto;
            }
        }
    }
}
