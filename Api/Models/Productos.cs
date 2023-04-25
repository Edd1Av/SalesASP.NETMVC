using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Productos
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Producto { get; set; }
        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }
        [Required]
        public float Precio { get; set; }
        [Required]
        public int Unidades { get; set; }
        [StringLength(300)]
        public string Imagen { get; set; }

        public List<ProductosVentas> ProductosVenta { get; set; }
        
    }
}