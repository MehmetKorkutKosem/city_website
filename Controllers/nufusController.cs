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
    public class nufusController : Controller
    {
        private BitlisEntities2 db = new BitlisEntities2();

        // GET: nufus
        public ActionResult Index()
        {
            return View(db.nufus.ToList());
        }

        // GET: nufus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nufu nufu = db.nufus.Find(id);
            if (nufu == null)
            {
                return HttpNotFound();
            }
            return View(nufu);
        }

        // GET: nufus/Create
        // Sadece admin erişebilir
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: nufus/Create
        // Sadece admin erişebilir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,nufusSayisi,yil")] nufu nufu)
        {
            if (ModelState.IsValid)
            {
                db.nufus.Add(nufu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nufu);
        }

        // GET: nufus/Edit/5
        // Sadece admin erişebilir
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nufu nufu = db.nufus.Find(id);
            if (nufu == null)
            {
                return HttpNotFound();
            }
            return View(nufu);
        }

        // POST: nufus/Edit/5
        // Sadece admin erişebilir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,nufusSayisi,yil")] nufu nufu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nufu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nufu);
        }

        // GET: nufus/Delete/5
        // Sadece admin erişebilir
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nufu nufu = db.nufus.Find(id);
            if (nufu == null)
            {
                return HttpNotFound();
            }
            return View(nufu);
        }

        // POST: nufus/Delete/5
        // Sadece admin erişebilir
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            nufu nufu = db.nufus.Find(id);
            db.nufus.Remove(nufu);
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
