using Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Api.Data
{
    public class ApiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ApiContext() : base("name=ApiContext")
        {
        }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            //modelBuilder.Entity<Ventas>().HasRequired(x => x.Producto);
            modelBuilder.Entity<ProductosVentas>().HasKey(x => new { x.VentasId, x.ProductosId });
            

        }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<ProductosVentas> ProductosVentas { get; set; }
    }
}
