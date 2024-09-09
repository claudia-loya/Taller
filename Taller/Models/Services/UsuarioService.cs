using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Taller.Models.Services
{
    public class UsuarioService
    {
        private readonly AESService aesService = new AESService();
        
        //Registra o Actualiza un usuario 
        public string SetUsuario(Usuario usuario)
        {
            if (usuario.Clave == usuario.ConfirmarClave)
            {
                usuario.Clave = aesService.Encrypt(usuario.Clave);
            }
            else {
                return "Las contraseñas no coinciden.";
            }
            return "OK";
        }
    }
}