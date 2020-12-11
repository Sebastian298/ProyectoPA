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
using Rotativa;

namespace ProyectoSistemaIntegral.Controllers
{
    public class ProductosController : Controller
    {
        private GestorContext db = new GestorContext();

        // GET: Productos
        public ActionResult Index(string strOrdenamiento, string currentFilter,string strBusqueda, int? pagina)
        {
            ViewBag.CurrentSort = strOrdenamiento;
            ViewBag.NombreSortParm = String.IsNullOrEmpty(strOrdenamiento) ? "nombre_desc" : "";
            ViewBag.DateSortParm = strOrdenamiento == "Fecha" ? "fecha_desc" : "Fecha";
            if (strBusqueda != null)
            {
                pagina = 1;
            }
            else
                strBusqueda = currentFilter;

            var productos = db.Productos.AsEnumerable();
            productos = from s in db.Productos
                        select s;
            if (!String.IsNullOrEmpty(strBusqueda))
            {
                productos = productos.Where(a => a.Nombre.Contains(strBusqueda));
            }
            switch (strOrdenamiento)
            {
                case "nombre_desc":
                    productos = productos.OrderByDescending(s => s.Nombre);
                    break;
                case "Fecha":
                    productos = productos.OrderBy(s => s.FechaIngreso);
                    break;
                case "fecha_desc":
                    productos = productos.OrderByDescending(s => s.FechaIngreso);
                    break;
                default:
                    productos = productos.OrderBy(s => s.Nombre);
                    break;
            }
            //return View(productos.ToList());
            int pageSize = 4;
            int pageNumber = (pagina ?? 1);
            return View(productos.ToPagedList(pageNumber, pageSize));
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.Categoria = new SelectList(db.Categorias, "ID", "Nombre");
            ViewBag.Proveedor = new SelectList(db.Proveedores, "Id", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductoId,Nombre,Descripcion,Precio,Categoria,Proveedor,FechaIngreso")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categoria = new SelectList(db.Categorias, "ID", "Nombre", productos.Categoria);
            ViewBag.Proveedor = new SelectList(db.Proveedores, "Id", "Nombre", productos.Proveedor);
            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categoria = new SelectList(db.Categorias, "ID", "Nombre", productos.Categoria);
            ViewBag.Proveedor = new SelectList(db.Proveedores, "Id", "Nombre", productos.Proveedor);
            return View(productos);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductoId,Nombre,Descripcion,Precio,Categoria,Proveedor,FechaIngreso")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categoria = new SelectList(db.Categorias, "ID", "Nombre", productos.Categoria);
            ViewBag.Proveedor = new SelectList(db.Proveedores, "Id", "Nombre", productos.Proveedor);
            return View(productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productos productos = db.Productos.Find(id);
            db.Productos.Remove(productos);
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

        public ActionResult Reporte()
        {
            var productos = db.Productos.AsEnumerable();
            productos = from s in db.Productos
                        where s.FechaIngreso == DateTime.Today
                        select s;
            return View(productos.ToList());
        }

        public ActionResult Print()
        {
            return new ActionAsPdf("Reporte") { FileName = "Inventario.pdf" };
        }
    }
}
