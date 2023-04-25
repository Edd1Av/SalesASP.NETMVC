using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Refaccionaria.Models
{
    public class tablaDetailsViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int unidades { get; set; }
        public float precio  { get; set; }

        public float subtotal { get; set; }
    }
}