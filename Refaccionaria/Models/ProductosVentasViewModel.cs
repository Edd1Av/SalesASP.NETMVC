using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Refaccionaria.Models
{
    public class ProductosVentasView
    {
      
        public int ProductosId { get; set; }
        
        public int UnidadesVendidas { get; set; }
    }
    public class ProductosVentasViewModel
    {
        //public Array products;
        public List<ProductosVentasView> Products { get; set; }

    }

}