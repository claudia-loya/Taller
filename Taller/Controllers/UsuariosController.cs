using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Taller.Models;
using Taller.Models.Services;
using Taller.Permisos;

namespace Taller.Controllers
{
    [ValidarPermisos]
    public class UsuariosController : Controller
    {
        private readonly TallerEntities db = new TallerEntities();
        private readonly AESService aesService = new AESService();

        // GET: Usuarios
        public ActionResult Index()
        {
            Usuario usuario = Session["usuario"] as Usuario;

            if (usuario != null && usuario.EsAdmin != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.Usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            Usuario usuarioSesion = Session["usuario"] as Usuario;

            if (usuarioSesion != null && usuarioSesion.EsAdmin != 1)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            else
            {
                usuario.Clave = aesService.Decrypt(usuario.Clave);
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,Nombre,ApellidoPaterno,ApellidoMaterno,Telefono,Correo,Clave,EsAdmin")] Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Clave = aesService.Encrypt(usuario.Clave);
                usuario.EsAdmin = 1;
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            Usuarios usuario = new Usuarios();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                if (usuario.EsAdmin == 1)
                {
                    usuario = db.Usuarios.Find(id);
                   
                }
                else
                {
                    Usuario usuarioSesion = Session["usuario"] as Usuario;
                    usuario = db.Usuarios.Find(usuarioSesion.IdUsuario);
                }

                if (usuario == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    usuario.Clave = aesService.Decrypt(usuario.Clave);
                }

            }
           
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,Nombre,ApellidoPaterno,ApellidoMaterno,Telefono,Correo,Clave, EsAdmin")] Usuarios usuario)
        {
          
            var usuarioExistente = db.Usuarios.Find(usuario.IdUsuario);
            if (usuarioExistente == null)
            {
                return HttpNotFound();
            }

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.ApellidoPaterno = usuario.ApellidoPaterno;
            usuarioExistente.ApellidoMaterno = usuario.ApellidoMaterno;
            usuarioExistente.Telefono = usuario.Telefono;
            usuarioExistente.Correo = usuario.Correo;
            usuarioExistente.Clave = aesService.Encrypt(usuario.Clave);

            db.Entry(usuarioExistente).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
