using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Taller.Models.Services
{
    public class CarrosService
    {
        private readonly TallerEntities db = new TallerEntities();

        public GeneralModel RegistrarCarro( CarrosClientes carro)
        {
            
            GeneralModel resultado = new GeneralModel();

            if (carro.IdCliente == 0)
            {
                resultado.Mensaje= "Ocurrió un error al asignar el dueño del carro.";
                resultado.Exitoso = 0;
                return resultado;
            }

            var mensaje = new ObjectParameter("mensaje", typeof(string));
            var exitoso = new ObjectParameter("exitoso", typeof(int));

            db.SetCarroCliente(carro.IdCarroCliente, carro.IdCliente, carro.Modelo, carro.Marca, carro.Anio, carro.Placas, mensaje, exitoso);

            resultado.Mensaje = (string)mensaje.Value;
            resultado.Exitoso = (int)exitoso.Value;

            return resultado;
            

        }

        public GeneralModel EliminarCarro(int idCarroCliente)
        {

            GeneralModel resultado = new GeneralModel();

            if (idCarroCliente == 0)
            {
                resultado.Mensaje = "Ocurrió un error al seleccionar de carro a eliminar.";
                resultado.Exitoso = 0;
                return resultado;
            }

            var mensaje = new ObjectParameter("mensaje", typeof(string));
            var exitoso = new ObjectParameter("exitoso", typeof(int));

            db.DeleteCarroCliente(idCarroCliente, mensaje, exitoso);

            resultado.Mensaje = (string)mensaje.Value;
            resultado.Exitoso = (int)exitoso.Value;

            return resultado;


        }
    }
}