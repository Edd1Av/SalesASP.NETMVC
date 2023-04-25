using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Ventas
    {
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public float Total { get; set; }


        public List<ProductosVentas> ProductosVenta { get; set; }
    }
}