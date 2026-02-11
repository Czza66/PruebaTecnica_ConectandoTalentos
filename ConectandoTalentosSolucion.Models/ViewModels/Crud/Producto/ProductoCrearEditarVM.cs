using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConectandoTalentosSolucion.Models.ViewModels.Crud.Producto
{
    public class ProductoCrearEditarVM
    {
        public string? IdProducto { get; set; }

        [Required(ErrorMessage = "Es requerido que el producto tenga un nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "La descripcion de la categoria es obligatorio")]
        [Display(Name = "Descipcion de categoria")]
        public string descripcion { get; set; }

        public string? imgProducto { get; set; }

        [Required(ErrorMessage = "Es requerido que el producto tenga una cantidad disponible")]
        [Range(1, 200, ErrorMessage = "El valor debe estar entre 1 y 200")]
        public int cantidad { get; set; }

        [Required(ErrorMessage = "Es requerido que el producto tenga precio valor de venta")]
        [Column(TypeName = "decimal(18,0)")]
        [Range(1, 999999999999, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal valor { get; set; }

        [Display(Name = "Orden de visualizacion")]
        [Range(1, 100, ErrorMessage = "El valor debe estar entre 1 y 100")]
        public int orden { get; set; }

        public bool activo { get; set; } = true;

        public int CategoriaId { get; set; }
        public IEnumerable<SelectListItem>? ListaCategorias { get; set; }

    }
}
