using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSistemaIntegral.Models
{
    public class Ventas
    {
        [Key]
        public int VentaID { get; set; }
        [ForeignKey("Productos")]
        [Required(ErrorMessage = "Se requiere el nombre del producto)")]
        [DataType(DataType.Text)]
        [Display(Name = "Producto")]
        public int Producto { get; set; }
        public virtual Productos Productos { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de la venta")]
        public DateTime FechaVenta { get; set; }
    }
}