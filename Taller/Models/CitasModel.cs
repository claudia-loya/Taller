using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Taller.Models
{
    public class CitasModel
    {
        public partial class Cita
        {
            public int IdCita { get; set; }
            public int IdCarroCliente { get; set; }
            public Nullable<int> Aprobada { get; set; }
            public string Comentarios { get; set; }
            public Nullable<DateTime> FechaInicio { get; set; }
            public Nullable<DateTime> FechaFin { get; set; }
            public IEnumerable<SelectListItem> Carros { get; set; }
            public virtual CarrosClientes CarrosClientes { get; set; }
            
        }
    }
}