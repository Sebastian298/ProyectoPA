namespace ProyectoSistemaIntegral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modelos_Productos_Ventas_Proveedores_Categorias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 30, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 60, unicode: false),
                        Precio = c.Double(nullable: false),
                        Categoria = c.Int(nullable: false),
                        Proveedor = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductoId)
                .ForeignKey("dbo.Categorias", t => t.Categoria, cascadeDelete: true)
                .ForeignKey("dbo.Proveedores", t => t.Proveedor, cascadeDelete: true)
                .Index(t => t.Categoria)
                .Index(t => t.Proveedor);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 40, unicode: false),
                        Direccion = c.String(nullable: false, maxLength: 70, unicode: false),
                        CodigoPostal = c.String(nullable: false, maxLength: 5, unicode: false),
                        Telefono = c.String(nullable: false, maxLength: 10, unicode: false),
                        CorreoElectronico = c.String(nullable: false, maxLength: 80, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        VentaID = c.Int(nullable: false, identity: true),
                        Producto = c.Int(nullable: false),
                        FechaVenta = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VentaID)
                .ForeignKey("dbo.Productos", t => t.Producto, cascadeDelete: true)
                .Index(t => t.Producto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ventas", "Producto", "dbo.Productos");
            DropForeignKey("dbo.Productos", "Proveedor", "dbo.Proveedores");
            DropForeignKey("dbo.Productos", "Categoria", "dbo.Categorias");
            DropIndex("dbo.Ventas", new[] { "Producto" });
            DropIndex("dbo.Productos", new[] { "Proveedor" });
            DropIndex("dbo.Productos", new[] { "Categoria" });
            DropTable("dbo.Ventas");
            DropTable("dbo.Proveedores");
            DropTable("dbo.Productos");
            DropTable("dbo.Categorias");
        }
    }
}
