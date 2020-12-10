using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSistemaIntegral.Models
{
    public class Categorias
    {
        public int ID { get; set; }
        [Display(Name = "Nombre de la categoría")]
        [Column(TypeName = "varchar")]
        [MaxLength(30)]
        [Required(ErrorMessage = "Se requiere el nombre de la categoría")]
        public string Nombre { get; set; }
    }
}