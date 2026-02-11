using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio.IRepositorio;
using ConectandoTalentosSolucion.Models;
using ConectandoTalentosSolucion.Models.ViewModels.Crud.Producto;

namespace ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;

        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Categoria = new CategoriaRepository(_db);
            Producto = new ProductoRepository(_db);
        }

        public ICategoriaRepository Categoria { get;private set; }
        public IProductoRepository Producto { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }

}
