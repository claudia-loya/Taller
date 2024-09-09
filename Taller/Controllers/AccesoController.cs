using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Taller.Models;
using Taller.Models.Services;

namespace Taller.Controllers
{
    public class AccesoController : Controller
    {
        private AccesoService accesoService = new AccesoService();

        private TallerEntities db = new TallerEntities();


        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {

            GeneralModel resultado = accesoService.ValidarUsuario(usuario);

            if (resultado.IdUsuario != 0 && resultado.IdUsuario != null)
            {
                usuario.IdUsuario = resultado.IdUsuario;
                usuario.EsAdmin = resultado.EsAdmin;

                Session["usuario"] = usuario;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = resultado.Mensaje;
                return View();
            }


        }
    
        [HttpPost]
        public ActionResult Registrar(Usuario usuario) 
        {

            GeneralModel resultado = accesoService.RegistrarUsuario(usuario);

            if (resultado.Exitoso == 1)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                ViewData["Mensaje"] = resultado.Mensaje;
                return View();
            }
        }
    }
}