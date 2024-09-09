using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Taller.Models.Services
{
    public class AccesoService
    {
        private readonly AESService aesService = new AESService();
        private readonly TallerEntities db = new TallerEntities();
        public GeneralModel ValidarUsuario(Usuario usuario)
        {
            usuario.Clave = aesService.Encrypt(usuario.Clave);
            GeneralModel resultado = new GeneralModel();

            var mensaje = new ObjectParameter("mensaje", typeof(string));
            var exitoso = new ObjectParameter("exitoso", typeof(int));
            var idUsuario = new ObjectParameter("idUsuario", typeof(int));
            var esAdmin = new ObjectParameter("esAdmin", typeof(int));

            db.ValidarUsuario(usuario.Correo, usuario.Clave, mensaje, exitoso, idUsuario, esAdmin);

            resultado.Mensaje = (string)mensaje.Value;
            resultado.Exitoso = (int)exitoso.Value;
            resultado.IdUsuario = (int)idUsuario.Value;
            resultado.EsAdmin = (int)esAdmin.Value;

            return resultado;
        }
    
        public GeneralModel RegistrarUsuario(Usuario usuario)
        {
            GeneralModel resultado = new GeneralModel();
            usuario.EsAdmin = 0;
            if(usuario.Clave == usuario.ConfirmarClave)
            {
                usuario.Clave = aesService.Encrypt(usuario.Clave);

                var mensaje = new ObjectParameter("mensaje", typeof(string));
                var exitoso = new ObjectParameter("exitoso", typeof(int));

                db.SetUsuario(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Telefono, usuario.Correo, usuario.Clave, usuario.EsAdmin, mensaje, exitoso);

                resultado.Mensaje = (string)mensaje.Value;
                resultado.Exitoso = (int)exitoso.Value;

                return resultado;
            }
            else
            {
                resultado.Exitoso = 0;
                resultado.Mensaje = "Las contraseñas no coinciden";
                return resultado;
            }
            
        }
    }

}