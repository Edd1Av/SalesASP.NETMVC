using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class ProductosVentasView
    {
        [Required]
        public int ProductosId { get; set; }
        [Required]
        public int UnidadesVendidas { get; set; }
    }
    public class ProductosVentasViewModel
    {
        //public Array products;
        public List<ProductosVentasView> Products { get; set; }
        
    }

    
}