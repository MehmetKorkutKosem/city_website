using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    // Bu controller'da giriş yapmış kullanıcıların erişim sağlayabilmesi için [Authorize] filtresi ekliyoruz.
    [Authorize]
    public class IlcesController : Controller
    {
        private BitlisEntities2 db = new BitlisEntities2();

        // GET: Ilces
        public ActionResult Index()
        {
            return View(db.Ilces.ToList());
        }

        // GET: Ilces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilce ilce = db.Ilces.Find(id);
            if (ilce == null)
            {
                return HttpNotFound();
            }
            return View(ilce);
        }

        // GET: Ilces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ilces/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ilceAdi,aciklama")] Ilce ilce)
        {
            if (ModelState.IsValid)
            {
                db.Ilces.Add(ilce);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ilce);
        }

        // GET: Ilces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilce ilce = db.Ilces.Find(id);
            if (ilce == null)
            {
                return HttpNotFound();
            }
            return View(ilce);
        }

        // POST: Ilces/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ilceAdi,aciklama")] Ilce ilce)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ilce).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ilce);
        }

        // GET: Ilces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilce ilce = db.Ilces.Find(id);
            if (ilce == null)
            {
                return HttpNotFound();
            }
            return View(ilce);
        }

        // POST: Ilces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ilce ilce = db.Ilces.Find(id);
            db.Ilces.Remove(ilce);
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
