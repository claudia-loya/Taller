//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Taller
{
    using System;
    using System.Collections.Generic;
    
    public partial class Citas
    {
        public int IdCita { get; set; }
        public int IdCarroCliente { get; set; }
        public Nullable<int> Aprobada { get; set; }
        public string Comentarios { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
    
        public virtual CarrosClientes CarrosClientes { get; set; }
    }
}
