using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace Taller.Models.Services
{
    public class CitasService
    {
        private readonly TallerEntities db = new TallerEntities();

        public List<GetCitas_Result> GetCitas(int? idCita, int? aprobada, int? idCliente)
        {

            List<GetCitas_Result> citas = new List<GetCitas_Result>();

            citas = db.GetCitas(idCita, aprobada, idCliente).ToList();

            return citas;

        }

        public GeneralModel SetCita(Citas cita)
        {

            GeneralModel resultado = new GeneralModel();

            if (cita.IdCarroCliente == 0 && cita.IdCita == 0)
            {
                resultado.Mensaje = "Ocurrió un error al asignar el carro.";
                resultado.Exitoso = 0;
                return resultado;
            }

            var mensaje = new ObjectParameter("mensaje", typeof(string));
            var exitoso = new ObjectParameter("exitoso", typeof(int));


            db.SetCita(cita.IdCita, cita.IdCarroCliente, cita.Aprobada, cita.Comentarios, cita.FechaInicio, mensaje, exitoso);

            resultado.Mensaje = (string)mensaje.Value;
            resultado.Exitoso = (int)exitoso.Value;

            return resultado;


        }

        public GeneralModel EliminarCita(int idCita)
        {

            GeneralModel resultado = new GeneralModel();

            if (idCita == 0)
            {
                resultado.Mensaje = "Ocurrió un error al seleccionar la cita a eliminar.";
                resultado.Exitoso = 0;
                return resultado;
            }

            var mensaje = new ObjectParameter("mensaje", typeof(string));
            var exitoso = new ObjectParameter("exitoso", typeof(int));

            db.DeleteCita(idCita, mensaje, exitoso);

            resultado.Mensaje = (string)mensaje.Value;
            resultado.Exitoso = (int)exitoso.Value;

            return resultado;
        }
    }
}