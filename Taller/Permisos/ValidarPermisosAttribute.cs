using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Taller.Permisos
{
    public class ValidarPermisosAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["usuario"] == null) {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}