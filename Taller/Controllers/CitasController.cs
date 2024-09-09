using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Taller.Models;
using Taller.Models.Services;
using Taller.Permisos;
using static Taller.Models.CitasModel;

namespace Taller.Controllers
{
    [ValidarPermisos]
    public class CitasController : Controller
    {

        private TallerEntities db = new TallerEntities();
        private CitasService citasService = new CitasService();
        private CarrosService carrosService = new CarrosService();
        // GET: Citas
        public ActionResult Index()
        {
            Usuario usuario = Session["usuario"] as Usuario;
            List<GetCitas_Result> lista = new List<GetCitas_Result>();

            if (usuario != null && usuario.EsAdmin != 1)
            {
                lista = citasService.GetCitas(null, null, usuario.IdUsuario); 
            }
            else if(usuario != null && usuario.EsAdmin == 1)
            {
                lista = citasService.GetCitas(null, null, null);
            }
            return View(lista);
        }

        // GET: Citas/Create
        public ActionResult Create()
        {

            Usuario usuario = Session["usuario"] as Usuario;

            var carros = db.GetCarrosClientes(usuario.IdUsuario).Select(c => new SelectListItem
            {
                Value = c.IdCarroCliente.ToString(), 
                Text = $"{c.Marca} {c.Modelo}" 
            }).ToList();

            var viewModel = new Cita
            {
                Carros = carros,
                FechaInicio = DateTime.Now
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Citas cita)
        {

            GeneralModel resultado = citasService.SetCita(cita);

            if (resultado.Exitoso == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["Mensaje"] = resultado.Mensaje;

                Usuario usuario = Session["usuario"] as Usuario;

                //Obtiene el listado de carros y agrega manejo para poder usarlo en la vista
                var carros = db.GetCarrosClientes(usuario.IdUsuario).Select(c => new SelectListItem
                {
                    Value = c.IdCarroCliente.ToString(), 
                    Text = $"{c.Marca} {c.Modelo}" 
                }).ToList();

                var viewModel = new Cita
                {
                    Carros = carros,
                    FechaInicio = cita.FechaInicio
                };

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult AprobarRechazar(string idCita, string comentarios, string aprobada)
        {
            Citas cita = new Citas();
            cita.IdCita = Int32.Parse(idCita);
            cita.Comentarios = comentarios;
            cita.Aprobada = Int32.Parse(aprobada);
            cita.FechaInicio = DateTime.Now;
            GeneralModel resultado = citasService.SetCita(cita);

            if (resultado.Exitoso == 1)
            {
                //Retorna json para utilizarlo en el modal de aprobación/rechazo
                return Json(new { success = true });
            }
            else
            {
                //Retorna json para utilizarlo en el modal de aprobación/rechazo
                return Json(new { success = false, mensaje = resultado.Mensaje });
            }
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<GetCitas_Result> citas = citasService.GetCitas(id, null, null);

            GetCitas_Result cita = citas.FirstOrDefault();

            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GeneralModel resultado = citasService.EliminarCita(id);

            if (resultado.Exitoso == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["Mensaje"] = resultado.Mensaje;
                return View();
            }
        }

    }
}