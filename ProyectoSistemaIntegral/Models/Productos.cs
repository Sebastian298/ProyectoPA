using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSistemaIntegral.Models
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }


        [Display(Name = "Nombre del producto")]
        [Column(TypeName = "varchar")]
        [MaxLength(30)]
        [Required(ErrorMessage = "Se requiere el nombre del producto")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción del producto")]
        [Column(TypeName = "varchar")]
        [MaxLength(60)]
        [Required(ErrorMessage = "Se requiere la descripción del producto")]
        public string Descripcion { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Precio del producto")]
        [Required(ErrorMessage = "Se requiere el precio del producto")]
        public double Precio { get; set; }

        [ForeignKey("Categorias")]
        [Required(ErrorMessage = "Se requiere la categoria del producto")]
        [DataType(DataType.Text)]
        [Display(Name = "Categoria")]
        public int Categoria { get; set; }
        public virtual Categorias Categorias { get; set; }

        [ForeignKey("Proveedores")]
        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "Se requiere el proveedor del producto")]
        [DataType(DataType.Text)]
        public int Proveedor { get; set; }
        public virtual Proveedores Proveedores { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Ingreso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso { get; set; }
    }
}