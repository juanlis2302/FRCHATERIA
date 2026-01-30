using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ferre2.Models;

namespace ferre2.Controllers
{
    public class detalles_facturaController : Controller
    {
        private ferre2DBEntities1 db = new ferre2DBEntities1();

        // GET: detalles_factura
        public ActionResult Index()
        {
            var detalles_factura = db.detalles_factura.Include(d => d.facturas).Include(d => d.productos);
            return View(detalles_factura.ToList());
        }

        // GET: detalles_factura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalles_factura detalles_factura = db.detalles_factura.Find(id);
            if (detalles_factura == null)
            {
                return HttpNotFound();
            }
            return View(detalles_factura);
        }

        // GET: detalles_factura/Create
        public ActionResult Create()
        {
            ViewBag.id_factura = new SelectList(db.facturas, "id_factura", "numero_factura");
            ViewBag.id_producto = new SelectList(db.productos, "id_producto", "codigo");
            return View();
        }

        // POST: detalles_factura/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_detalle,id_factura,id_producto,descripcion,cantidad,precio_unitario,descuento_porcentaje,descuento_valor,iva_porcentaje,iva_valor,subtotal,total_linea")] detalles_factura detalles_factura)
        {
            if (ModelState.IsValid)
            {
                db.detalles_factura.Add(detalles_factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_factura = new SelectList(db.facturas, "id_factura", "numero_factura", detalles_factura.id_factura);
            ViewBag.id_producto = new SelectList(db.productos, "id_producto", "codigo", detalles_factura.id_producto);
            return View(detalles_factura);
        }

        // GET: detalles_factura/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalles_factura detalles_factura = db.detalles_factura.Find(id);
            if (detalles_factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_factura = new SelectList(db.facturas, "id_factura", "numero_factura", detalles_factura.id_factura);
            ViewBag.id_producto = new SelectList(db.productos, "id_producto", "codigo", detalles_factura.id_producto);
            return View(detalles_factura);
        }

        // POST: detalles_factura/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_detalle,id_factura,id_producto,descripcion,cantidad,precio_unitario,descuento_porcentaje,descuento_valor,iva_porcentaje,iva_valor,subtotal,total_linea")] detalles_factura detalles_factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalles_factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_factura = new SelectList(db.facturas, "id_factura", "numero_factura", detalles_factura.id_factura);
            ViewBag.id_producto = new SelectList(db.productos, "id_producto", "codigo", detalles_factura.id_producto);
            return View(detalles_factura);
        }

        // GET: detalles_factura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalles_factura detalles_factura = db.detalles_factura.Find(id);
            if (detalles_factura == null)
            {
                return HttpNotFound();
            }
            return View(detalles_factura);
        }

        // POST: detalles_factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detalles_factura detalles_factura = db.detalles_factura.Find(id);
            db.detalles_factura.Remove(detalles_factura);
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
