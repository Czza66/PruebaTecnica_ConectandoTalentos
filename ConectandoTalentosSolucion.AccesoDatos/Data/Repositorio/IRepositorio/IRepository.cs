using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConectandoTalentosSolucion.AccesoDatos.Data.Repositorio.IRepositorio
{
    public interface IRepository<T> where T : class
    {
        //Obtener registro por su ID
        T Get(int id);

        //Lista de Registros
        IQueryable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderbBy = null,
            string? includeProperties = null
            );

        //Obtener un registro unico
        T GetFirstOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string? includeproperties = null
            );

        //Agregar un registro
        void Add(T entity);

        //Eliminar un registro
        void Remove(int id);

    }
}
