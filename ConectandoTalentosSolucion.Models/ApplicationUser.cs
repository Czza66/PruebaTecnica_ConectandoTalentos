using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ConectandoTalentosSolucion.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage ="El primer nombre es requerido")]
        public string Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        [Required(ErrorMessage = "El primer apellido es requerido")]
        public string Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        [Required(ErrorMessage ="El numero de identificacion es requerido")]
        public int NumIdentificacion { get; set; }
        [Required(ErrorMessage = "La direccion de residencia es requerida")]
        public string  Direccion { get; set; }
        [Required(ErrorMessage = "La ciudad de residencia es requerida")]
        public string Ciudad { get; set; }

    }
}
