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
    public class productosController : Controller
    {
        private ferre2DBEntities1 db = new ferre2DBEntities1();

        [Authorize]

        // GET: productos
        public ActionResult Index()
        {
            return View(db.productos.ToList());
        }

        // GET: productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_producto,codigo,nombre,descripcion,precio_base,iva_porcentaje,categoria,stock,estado,fecha_creacion")] productos productos)
        {
            if (ModelState.IsValid)
            {
                // SOLUCIÓN AL ERROR DATETIME:
                // Si la fecha llega vacía (0001), le asignamos la fecha actual del servidor
                if (productos.fecha_creacion < (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue)
                {
                    productos.fecha_creacion = DateTime.Now;
                }

                try
                {
                    db.productos.Add(productos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Esto captura errores inesperados al guardar
                    ModelState.AddModelError("", "Error al guardar en BD: " + ex.Message);
                }
            }

            // ESTE ES EL ELSE QUE BUSCAS:
            // Si llegamos aquí, algo falló. Vamos a inspeccionar los errores.
            var errores = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errores)
            {
                // Esto aparecerá en el "Resumen de Errores" de tu Vista (ValidationSummary)
                System.Diagnostics.Debug.WriteLine("Error detectado: " + error.ErrorMessage);
            }

            return View(productos);
        }

        // GET: productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_producto,codigo,nombre,descripcion,precio_base,iva_porcentaje,categoria,stock,estado,fecha_creacion")] productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productos);
        }

        // GET: productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productos productos = db.productos.Find(id);
            db.productos.Remove(productos);
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
