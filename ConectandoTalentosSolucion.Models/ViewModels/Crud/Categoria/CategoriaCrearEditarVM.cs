using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectandoTalentosSolucion.Models.ViewModels.Crud.Categoria
{
    public class CategoriaCrearEditarVM
    {
        [Required(ErrorMessage = "El nombre de la categoria es obligatorio")]
        [MaxLength(60)]
        [Display(Name = "Nombre de categoria")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "La descripcion de la categoria es obligatorio")]
        [Display(Name = "Descripcion de categoria")]
        public string descripcion { get; set; }

        [Display(Name = "Orden de visualizacion")]
        [Range(1, 10, ErrorMessage = "El valor debe estar entre 1 y 100")]
        [Required(ErrorMessage ="El orden de visualizacion es requerido")]
        public int orden { get; set; }

        public string? imgCategoria { get; set; }

        public bool activo { get; set; } = true;

        public string? CategoriaId { get; set; }
    }
}
