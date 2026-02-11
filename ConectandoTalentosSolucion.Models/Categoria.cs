using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectandoTalentosSolucion.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre de la categoria es obligatorio")]
        [MaxLength(60)]
        [Display(Name="Nombre de categoria")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "La descripcion de la categoria es obligatorio")]
        [Display(Name = "Descipcion de categoria")]
        public string descripcion { get; set; }

        [Display(Name = "Orden de visualizacion")]
        [Range(1,100, ErrorMessage ="El valor debe estar entre 1 y 100")]
        public int orden { get; set; }

        [Required(ErrorMessage ="La categoria debe contener una imagen para su identificacion")]
        public string imgCategoria { get; set; }

        public DateTime fechaCreacion { get; set; }

        public DateTime fechaModificacion { get; set; }

        public bool activo { get; set; } = true;
    }
}
