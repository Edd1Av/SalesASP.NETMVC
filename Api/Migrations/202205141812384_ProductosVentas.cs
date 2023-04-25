namespace Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductosVentas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Producto = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(nullable: false, maxLength: 200),
                        Precio = c.Single(nullable: false),
                        Unidades = c.Int(nullable: false),
                        Imagen = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductosVentas",
                c => new
                    {
                        VentasId = c.Int(nullable: false),
                        ProductosId = c.Int(nullable: false),
                        UnidadesVendidas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VentasId, t.ProductosId })
                .ForeignKey("dbo.Productos", t => t.ProductosId, cascadeDelete:false)
                .ForeignKey("dbo.Ventas", t => t.VentasId, cascadeDelete: true)
                .Index(t => t.VentasId)
                .Index(t => t.ProductosId);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductosVentas", "VentasId", "dbo.Ventas");
            DropForeignKey("dbo.ProductosVentas", "ProductosId", "dbo.Productos");
            DropIndex("dbo.ProductosVentas", new[] { "ProductosId" });
            DropIndex("dbo.ProductosVentas", new[] { "VentasId" });
            DropTable("dbo.Ventas");
            DropTable("dbo.ProductosVentas");
            DropTable("dbo.Productos");
        }
    }
}
