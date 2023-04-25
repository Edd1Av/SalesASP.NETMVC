using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Refaccionaria.Models
{
    public class ProductosVentas
    {
        [Display(Name = "# Venta")]
        public int VentasId { get; set; }
        [Display(Name = "# Producto")]
        public int ProductosId { get; set; }
        public int UnidadesVendidas { get; set; }
    }
}
