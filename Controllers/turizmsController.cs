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
    // Giriş yapmış kullanıcıların erişebileceği şekilde genel [Authorize] filtresi eklendi.
    [Authorize]
    public class turizmsController : Controller
    {
        private BitlisEntities2 db = new BitlisEntities2();

        // GET: turizms
        // Bu işlem herkese açık; yani, giriş yapmış tüm kullanıcılar bu sayfayı görebilir.
        public ActionResult Index()
        {
            return View(db.turizms.ToList());
        }

        // GET: turizms/Details/5
        // Bu işlem de herkese açık; yani, giriş yapmış tüm kullanıcılar bu sayfayı görebilir.
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            turizm turizm = db.turizms.Find(id);
            if (turizm == null)
            {
                return HttpNotFound();
            }
            return View(turizm);
        }

        // GET: turizms/Create
        // Sadece admin erişebilir
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: turizms/Create
        // Sadece admin erişebilir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,yerAdi,aciklama")] turizm turizm)
        {
            if (ModelState.IsValid)
            {
                db.turizms.Add(turizm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(turizm);
        }

        // GET: turizms/Edit/5
        // Sadece admin erişebilir
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            turizm turizm = db.turizms.Find(id);
            if (turizm == null)
            {
                return HttpNotFound();
            }
            return View(turizm);
        }

        // POST: turizms/Edit/5
        // Sadece admin erişebilir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,yerAdi,aciklama")] turizm turizm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turizm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(turizm);
        }

        // GET: turizms/Delete/5
        // Sadece admin erişebilir
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            turizm turizm = db.turizms.Find(id);
            if (turizm == null)
            {
                return HttpNotFound();
            }
            return View(turizm);
        }

        // POST: turizms/Delete/5
        // Sadece admin erişebilir
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            turizm turizm = db.turizms.Find(id);
            db.turizms.Remove(turizm);
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
