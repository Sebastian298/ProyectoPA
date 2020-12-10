namespace ProyectoSistemaIntegral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Usuarios_Delete : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Usuarios");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 20, unicode: false),
                        Password = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
