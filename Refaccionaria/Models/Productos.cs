using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Refaccionaria.Models
{
    public class Productos
    {
        [Display(Name = "# Producto")]
        public int Id { get; set; }
   
        public string Producto { get; set; }

        public string Descripcion { get; set; }

        public float Precio { get; set; }
   
        public int Unidades { get; set; }

        public string Imagen { get; set; }


    }
}