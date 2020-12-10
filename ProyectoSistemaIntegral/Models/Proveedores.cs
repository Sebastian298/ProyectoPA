using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSistemaIntegral.Models
{
    public class Proveedores
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del proveedor")]
        [Column(TypeName = "varchar")]
        [MaxLength(40)]
        [Required(ErrorMessage = "Se requiere el nombre del proveedor")]
        public string Nombre { get; set; }

        [Display(Name = "Dirección del proveedor")]
        [Column(TypeName = "varchar")]
        [MaxLength(70)]
        [Required(ErrorMessage = "Se requiere la dirección del proveedor")]
        public string Direccion { get; set; }

        [Display(Name = "Código Postal")]
        [Column(TypeName = "varchar")]
        [MaxLength(5, ErrorMessage = "El código postal no debe pasar de 5 digitos")]
        [Required(ErrorMessage = "Capture el código postal")]
        public string CodigoPostal { get; set; }

        [Phone]
        [Display(Name = "Telefono del proveedor")]
        [Column(TypeName = "varchar")]
        [MaxLength(10, ErrorMessage = "El telefono no debe pasar de 10 digitos")]
        [Required(ErrorMessage = "Capture el telefono")]
        public string Telefono { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Capture el correo electronico")]
        [Column(TypeName = "varchar")]
        [MaxLength(80)]
        public string CorreoElectronico { get; set; }
    }
}