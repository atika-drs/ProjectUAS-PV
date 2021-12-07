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
    public class pembayaransController : Controller
    {
        private uasEntities db = new uasEntities();

        // GET: pembayarans
        public ActionResult Index()
        {
            var pembayaran = db.pembayaran.Include(p => p.reservasi);
            return View(pembayaran.ToList());
        }

        // GET: pembayarans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pembayaran pembayaran = db.pembayaran.Find(id);
            if (pembayaran == null)
            {
                return HttpNotFound();
            }
            return View(pembayaran);
        }

        // GET: pembayarans/Create
        public ActionResult Create()
        {
            ViewBag.ReservasiID = new SelectList(db.reservasi, "ReservasiID", "ReservasiID");
            return View();
        }

        // POST: pembayarans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PembayaranID,ReservasiID,tgl_bayar,total_bayar,status_bayar")] pembayaran pembayaran)
        {
            if (ModelState.IsValid)
            {
                db.pembayaran.Add(pembayaran);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReservasiID = new SelectList(db.reservasi, "ReservasiID", "ReservasiID", pembayaran.ReservasiID);
            return View(pembayaran);
        }

        // GET: pembayarans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pembayaran pembayaran = db.pembayaran.Find(id);
            if (pembayaran == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservasiID = new SelectList(db.reservasi, "ReservasiID", "status_reserv", pembayaran.ReservasiID);
            return View(pembayaran);
        }

        // POST: pembayarans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PembayaranID,ReservasiID,tgl_bayar,total_bayar,status_bayar")] pembayaran pembayaran)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pembayaran).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservasiID = new SelectList(db.reservasi, "ReservasiID", "status_reserv", pembayaran.ReservasiID);
            return View(pembayaran);
        }

        // GET: pembayarans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pembayaran pembayaran = db.pembayaran.Find(id);
            if (pembayaran == null)
            {
                return HttpNotFound();
            }
            return View(pembayaran);
        }

        // POST: pembayarans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pembayaran pembayaran = db.pembayaran.Find(id);
            db.pembayaran.Remove(pembayaran);
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
