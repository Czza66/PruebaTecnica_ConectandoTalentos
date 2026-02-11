using ConectandoTalentosSolucion.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConectandoTalentosSolucion.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }
        //Agregar los modelos
        public DbSet<Categoria>categorias { get; set; } 
        public DbSet<Productos>productos { get; set; }
    }

}
