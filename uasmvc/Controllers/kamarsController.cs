using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using uasmvc.Models;

namespace uasmvc.Controllers
{
    public class kamarsController : Controller
    {
        private uasEntities db = new uasEntities();

        // GET: kamars
        public ActionResult Index()
        {
            return View(db.kamar.ToList());
        }

        // GET: kamars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kamar kamar = db.kamar.Find(id);
            if (kamar == null)
            {
                return HttpNotFound();
            }
            return View(kamar);
        }

        // GET: kamars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: kamars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KamarID,no_kamar,nama_kamar,harga_kamar,status_kamar")] kamar kamar)
        {
            if (ModelState.IsValid)
            {
                db.kamar.Add(kamar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kamar);
        }

        // GET: kamars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kamar kamar = db.kamar.Find(id);
            if (kamar == null)
            {
                return HttpNotFound();
            }
            return View(kamar);
        }

        // POST: kamars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KamarID,no_kamar,nama_kamar,harga_kamar,status_kamar")] kamar kamar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kamar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kamar);
        }

        // GET: kamars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kamar kamar = db.kamar.Find(id);
            if (kamar == null)
            {
                return HttpNotFound();
            }
            return View(kamar);
        }

        // POST: kamars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kamar kamar = db.kamar.Find(id);
            db.kamar.Remove(kamar);
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
