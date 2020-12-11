using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ProyectoSistemaIntegral.BD;
using ProyectoSistemaIntegral.Models;

namespace ProyectoSistemaIntegral.Controllers
{
    public class ProveedoresController : Controller
    {
        private GestorContext db = new GestorContext();

        // GET: Proveedores
        public ActionResult Index(string strOrdenamiento,string currentFilter, string strBusqueda,int? pagina)
        {
            ViewBag.CurrentSort = strOrdenamiento;
            ViewBag.NombreSortParm = String.IsNullOrEmpty(strOrdenamiento) ? "nombre_desc" : "";
            ViewBag.CorreoSortParm = strOrdenamiento == "Correo" ? "correo_desc" : "Correo";

            if (strBusqueda != null)
            {
                pagina = 1;
            }
            else
                strBusqueda = currentFilter;

            var proveedores = db.Proveedores.AsEnumerable();
            proveedores = from s in db.Proveedores
                        select s;
            if (!String.IsNullOrEmpty(strBusqueda))
            {
                proveedores = proveedores.Where(a => a.Nombre.Contains(strBusqueda)| a.CorreoElectronico.Contains(strBusqueda));
            }
            switch (strOrdenamiento)
            {
                case "nombre_desc":
                    proveedores = proveedores.OrderByDescending(s => s.Nombre);
                    break;
                case "Correo":
                    proveedores = proveedores.OrderBy(s => s.CorreoElectronico);
                    break;
                case "correo_desc":
                    proveedores = proveedores.OrderByDescending(s => s.CorreoElectronico);
                    break;
                default:
                    proveedores = proveedores.OrderBy(s => s.Nombre);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (pagina ?? 1);
            return View(proveedores.ToPagedList(pageNumber, pageSize));
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Direccion,CodigoPostal,Telefono,CorreoElectronico")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Proveedores.Add(proveedores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proveedores);
        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Direccion,CodigoPostal,Telefono,CorreoElectronico")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proveedores);
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedores proveedores = db.Proveedores.Find(id);
            db.Proveedores.Remove(proveedores);
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
