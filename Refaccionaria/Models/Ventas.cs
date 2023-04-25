using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Refaccionaria.Models
{
    public class Ventas
    {
        [Display(Name ="# Venta")]
        public int Id { get; set; }
 
        public DateTime Fecha { get; set; }
   
        public float Total { get; set; }

        public List<ProductosVentas> ProductosVenta { get; set; }

    }
}