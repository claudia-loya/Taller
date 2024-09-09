using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taller.Models.Services;
using Taller.Permisos;


namespace Taller.Controllers
{
    [ValidarPermisos]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Acceso");
        }
    }
}