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
    public class pelanggansController : Controller
    {
        private uasEntities db = new uasEntities();

        // GET: pelanggans
        public ActionResult Index()
        {
            return View(db.pelanggan.ToList());
        }

        // GET: pelanggans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelanggan pelanggan = db.pelanggan.Find(id);
            if (pelanggan == null)
            {
                return HttpNotFound();
            }
            return View(pelanggan);
        }

        // GET: pelanggans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: pelanggans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PelangganID,ktp_pelanggan,nama_pelanggan,email_pelanggan,telp_pelanggan,alamat_pelanggan")] pelanggan pelanggan)
        {
            if (ModelState.IsValid)
            {
                db.pelanggan.Add(pelanggan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pelanggan);
        }

        // GET: pelanggans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelanggan pelanggan = db.pelanggan.Find(id);
            if (pelanggan == null)
            {
                return HttpNotFound();
            }
            return View(pelanggan);
        }

        // POST: pelanggans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PelangganID,ktp_pelanggan,nama_pelanggan,email_pelanggan,telp_pelanggan,alamat_pelanggan")] pelanggan pelanggan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pelanggan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pelanggan);
        }

        // GET: pelanggans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelanggan pelanggan = db.pelanggan.Find(id);
            if (pelanggan == null)
            {
                return HttpNotFound();
            }
            return View(pelanggan);
        }

        // POST: pelanggans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pelanggan pelanggan = db.pelanggan.Find(id);
            db.pelanggan.Remove(pelanggan);
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
