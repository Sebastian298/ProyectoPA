using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoSistemaIntegral.BD
{
    public class GestorContext : DbContext
    {
        public GestorContext() : base("GestorContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<ProyectoSistemaIntegral.Models.Categorias> Categorias { get; set; }

        public System.Data.Entity.DbSet<ProyectoSistemaIntegral.Models.Productos> Productos { get; set; }

        public System.Data.Entity.DbSet<ProyectoSistemaIntegral.Models.Proveedores> Proveedores { get; set; }

        public System.Data.Entity.DbSet<ProyectoSistemaIntegral.Models.Ventas> Ventas { get; set; }
    }
}