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
    public class facturasController : Controller
    {
        private ferre2DBEntities1 db = new ferre2DBEntities1();

        [Authorize]
        // GET: facturas
        public ActionResult Index()
        {
            var facturas = db.facturas.Include(f => f.clientes);
            return View(facturas.ToList());
        }

        // GET: facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facturas facturas = db.facturas.Find(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            return View(facturas);
        }

        // GET: facturas/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.clientes, "id_cliente", "nombre_razon_social");
            return View();
        }

        // POST: facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_factura,numero_factura,fecha_emision,fecha_vencimiento,id_cliente,subtotal,total_impuestos,total_descuentos,total,estado,metodo_pago,notas,fecha_creacion,fecha_modificacion")] facturas facturas)
        {
            if (ModelState.IsValid)
            {
                db.facturas.Add(facturas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cliente = new SelectList(db.clientes, "id_cliente", "nombre_razon_social", facturas.id_cliente);
            return View(facturas);
        }

        // GET: facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facturas facturas = db.facturas.Find(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.clientes, "id_cliente", "nombre_razon_social", facturas.id_cliente);
            return View(facturas);
        }

        // POST: facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Edit([Bind(Include = "id_factura,numero_factura,fecha_emision,fecha_vencimiento,id_cliente,subtotal,total_impuestos,total_descuentos,total,estado,metodo_pago,notas,fecha_creacion,fecha_modificacion")] facturas facturas)
{
    if (ModelState.IsValid)
    {
        // --- FIX DE FECHAS ---
        // 1. Validar fecha_creacion (para que no de error si viene vacía desde la vista)
        if (facturas.fecha_creacion < (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue)
        {
            // Intentamos mantener la fecha original si es posible, o asignamos la actual
            facturas.fecha_creacion = DateTime.Now; 
        }

        // 2. Asignar automáticamente la fecha de modificación
        facturas.fecha_modificacion = DateTime.Now;

        try
        {
            db.Entry(facturas).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Captura el error real (como el de datetime2) y lo muestra en la vista
            var inner = ex.InnerException?.InnerException?.Message ?? ex.Message;
            ModelState.AddModelError("", "Error al actualizar: " + inner);
        }
    }

    // --- DEPURACIÓN DE ERRORES DE VALIDACIÓN ---
    var errores = ModelState.Values.SelectMany(v => v.Errors);
    foreach (var error in errores)
    {
        System.Diagnostics.Debug.WriteLine("Error en validación de Factura: " + error.ErrorMessage);
    }

    ViewBag.id_cliente = new SelectList(db.clientes, "id_cliente", "nombre_razon_social", facturas.id_cliente);
    return View(facturas);
}

        // GET: facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facturas facturas = db.facturas.Find(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            return View(facturas);
        }

        // POST: facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            facturas facturas = db.facturas.Find(id);
            db.facturas.Remove(facturas);
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
