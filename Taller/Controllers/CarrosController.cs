using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Taller.Models;
using Taller.Models.Services;
using Taller.Permisos;

namespace Taller.Controllers
{
    [ValidarPermisos]
    public class CarrosController : Controller
    {

        private TallerEntities db = new TallerEntities();
        private CarrosService carrosService = new CarrosService();

        // GET: Carros
        public ActionResult Index()
        {
            Usuario usuario = Session["usuario"] as Usuario;

            if (usuario != null && usuario.EsAdmin == 1)
            {
                return RedirectToAction("Index", "Home");

            }
            List<CarrosClientes> lista = db.GetCarrosClientes(usuario.IdUsuario).ToList();

            return View(db.GetCarrosClientes(usuario.IdUsuario).ToList());
        }

        // GET: Carros/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET : Carros/Edit/5
        public ActionResult Edit(int? id)
        {
            Usuario usuarioSesion = Session["usuario"] as Usuario;

            if (usuarioSesion != null && usuarioSesion.EsAdmin == 1)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            CarrosClientes carro = db.CarrosClientes.Find(id);

            
            if(carro.IdCliente != usuarioSesion.IdUsuario)
            {
                return RedirectToAction("Index");
            }
            if (carro == null)
            {
                return HttpNotFound();
            }
           
            return View(carro);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCarroCliente,IdCliente,Modelo, Marca, Anio, Placas")] CarrosClientes carro)
        {
            if (ModelState.IsValid)
            {
                Usuario usuarioSesion = Session["usuario"] as Usuario;
                carro.IdCliente = usuarioSesion.IdUsuario ?? 0;

            }

            GeneralModel resultado = carrosService.RegistrarCarro(carro);


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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCarroCliente, IdCliente, Modelo, Marca, Anio, Placas")] CarrosClientes carro)
        {

            if (ModelState.IsValid)
            {
                Usuario usuarioSesion = Session["usuario"] as Usuario;
                carro.IdCliente = usuarioSesion.IdUsuario ?? 0;

            }
            GeneralModel resultado = carrosService.RegistrarCarro(carro);


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


        // GET: Carros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarrosClientes carro = db.CarrosClientes.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            return View(carro);
        }

        // POST: Carros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GeneralModel resultado = carrosService.EliminarCarro(id);


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