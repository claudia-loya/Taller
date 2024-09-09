using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taller.Models
{
    public class Usuario
    {
        public int? IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public long Telefono { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
        public Nullable<int> EsAdmin { get; set; }

    }
}