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
    public class VentasController : Controller
    {
        private GestorContext db = new GestorContext();

        // GET: Ventas
        public ActionResult Index(string strOrdenamiento, string currentFilter, string strBusqueda, int? pagina)
        {
            ViewBag.CurrentSort = strOrdenamiento;
            ViewBag.DateSortParm = String.IsNullOrEmpty(strOrdenamiento) ? "date_desc" : "";
            ViewBag.PrecioSortParam = strOrdenamiento == "Precio" ? "precio_desc" : "Precio";
            if (strBusqueda != null)
            {
                pagina = 1;
            }
            else
                strBusqueda = currentFilter;
            var ventas = db.Ventas.AsEnumerable();
            ventas = from v in db.Ventas
                     select v;
            if (!String.IsNullOrEmpty(strBusqueda))
            {
                ventas = ventas.Where(a => a.FechaVenta==DateTime.Parse(strBusqueda));
            }
            switch (strOrdenamiento)
            {
                case "date_desc":
                    ventas = ventas.OrderBy(s => s.FechaVenta);
                    break;
                default:
                    ventas = ventas.OrderByDescending(s => s.FechaVenta);
                    break;
            }
            //return View(productos.ToList());
            int pageSize = 4;
            int pageNumber = (pagina ?? 1);
            return View(ventas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Ventas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventas ventas = db.Ventas.Find(id);
            if (ventas == null)
            {
                return HttpNotFound();
            }
            return View(ventas);
        }

        // GET: Ventas/Create
        public ActionResult Create()
        {
            ViewBag.Producto = new SelectList(db.Productos, "ProductoId", "Nombre");
            return View();
        }

        // POST: Ventas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VentaID,Producto,FechaVenta")] Ventas ventas)
        {
            if (ModelState.IsValid)
            {
                db.Ventas.Add(ventas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Producto = new SelectList(db.Productos, "ProductoId", "Nombre", ventas.Producto);
            return View(ventas);
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventas ventas = db.Ventas.Find(id);
            if (ventas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Producto = new SelectList(db.Productos, "ProductoId", "Nombre", ventas.Producto);
            return View(ventas);
        }

        // POST: Ventas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VentaID,Producto,FechaVenta")] Ventas ventas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ventas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Producto = new SelectList(db.Productos, "ProductoId", "Nombre", ventas.Producto);
            return View(ventas);
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventas ventas = db.Ventas.Find(id);
            if (ventas == null)
            {
                return HttpNotFound();
            }
            return View(ventas);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ventas ventas = db.Ventas.Find(id);
            db.Ventas.Remove(ventas);
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
            var ventas = db.Ventas.AsEnumerable();
            ventas = from s in db.Ventas
                     where s.FechaVenta == DateTime.Today
                        select s;
            return View(ventas.ToList());
        }

        public ActionResult Print()
        {
            return new ActionAsPdf("Reporte") { FileName = "Ventas.pdf" };
        }
    }
}
