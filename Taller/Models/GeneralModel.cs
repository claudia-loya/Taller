using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taller.Models
{
    public class GeneralModel
    {
        public string Mensaje { get; set; }
        public int? Exitoso { get; set; }
        public int? IdUsuario { get; set; }
        public int? EsAdmin { get; set; }
    }
}