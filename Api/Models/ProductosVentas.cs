using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class ProductosVentas
    {
        public int VentasId { get; set; }
        public int ProductosId { get; set; }
        [Required]
        public int UnidadesVendidas { get; set; }
    }
}