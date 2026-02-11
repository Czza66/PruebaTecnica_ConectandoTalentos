using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio.IRepositorio;
using ConectandoTalentosSolucion.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db):base(db)
        {
            _db = db;   
        }
        public IEnumerable<SelectListItem> GetListaCategorias() 
        {
            return _db.categorias
                .Where(c => c.activo == true)
                .Select(c => new SelectListItem
                {
                    Text = c.nombre,
                    Value = c.Id.ToString()
                });
        }

        public void Update(Categoria categoria) 
        {
            var objDesdeDB = _db.categorias.FirstOrDefault(c => c.Id == categoria.Id);
            if (objDesdeDB != null) 
            {
                objDesdeDB.nombre = categoria.nombre;
                objDesdeDB.descripcion = categoria.descripcion;
                objDesdeDB.orden = categoria.orden;
                objDesdeDB.imgCategoria = categoria.imgCategoria;
                objDesdeDB.activo = categoria.activo;
                objDesdeDB.fechaModificacion = categoria.fechaModificacion;
            }
        }

    }
}
