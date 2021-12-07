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
    public class reservasisController : Controller
    {
        private uasEntities db = new uasEntities();

        // GET: reservasis
        public ActionResult Index()
        {
            var reservasi = db.reservasi.Include(r => r.kamar).Include(r => r.pelanggan);
            return View(reservasi.ToList());
        }

        // GET: reservasis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservasi reservasi = db.reservasi.Find(id);
            if (reservasi == null)
            {
                return HttpNotFound();
            }
            return View(reservasi);
        }

        // GET: reservasis/Create
        public ActionResult Create()
        {
            var context = new uasEntities();
            ViewBag.KamarID = context.kamar.Where(y => y.status_kamar == "kosong")
                              .Select(x => new SelectListItem{
                                  Value = x.KamarID.ToString(),
                                  Text = x.nama_kamar,
                              });
            //ViewBag.tgtl_reserv = DateTime.Now;
            //ViewBag.KamarID = new SelectList(db.kamar, "KamarID", "no_kamar");
            ViewBag.PelangganID = new SelectList(db.pelanggan, "PelangganID", "nama_pelanggan");
            return View();
        }

        // POST: reservasis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservasiID,KamarID,PelangganID,tgtl_reserv,tgl_masuk_reserv,tgl_keluar_reserv,lama_reserv,status_reserv")] reservasi reservasi,
            [Bind(Include = "PembayaranID,ReservasiID,tgl_bayar,total_bayar,status_bayar")] pembayaran pembayaran)
        {
            if (ModelState.IsValid)
            {
                db.reservasi.Add(reservasi);
                var context = new uasEntities();
                int roompick = reservasi.KamarID;
                var room = (from a in context.kamar
                            where a.KamarID == roompick
                            select a).Single();
                room.status_kamar = "penuh";
                context.SaveChanges();

                pembayaran.tgl_bayar = DateTime.Now;
                pembayaran.total_bayar = room.harga_kamar * reservasi.lama_reserv;
                pembayaran.status_bayar = "belum";
                db.pembayaran.Add(pembayaran);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KamarID = new SelectList(db.kamar, "KamarID", "no_kamar", reservasi.KamarID);
            ViewBag.PelangganID = new SelectList(db.pelanggan, "PelangganID", "ktp_pelanggan", reservasi.PelangganID);
            return View(reservasi);
        }

        // GET: reservasis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservasi reservasi = db.reservasi.Find(id);
            if (reservasi == null)
            {
                return HttpNotFound();
            }
            ViewBag.KamarID = new SelectList(db.kamar, "KamarID", "no_kamar", reservasi.KamarID);
            ViewBag.PelangganID = new SelectList(db.pelanggan, "PelangganID", "nama_pelanggan", reservasi.PelangganID);
            return View(reservasi);
        }

        // POST: reservasis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservasiID,KamarID,PelangganID,tgtl_reserv,tgl_masuk_reserv,tgl_keluar_reserv,lama_reserv,status_reserv")] reservasi reservasi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservasi).State = EntityState.Modified;
                db.SaveChanges();
                var context = new uasEntities();
                int roompick = reservasi.KamarID;
                var room = (from a in context.kamar
                            where a.KamarID == roompick
                            select a).Single();
                if (reservasi.status_reserv == "out")
                {
                    room.status_kamar = "kosong";
                    context.SaveChanges();
                }
                else
                {
                    room.status_kamar = "penuh";
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.KamarID = new SelectList(db.kamar, "KamarID", "no_kamar", reservasi.KamarID);
            ViewBag.PelangganID = new SelectList(db.pelanggan, "PelangganID", "ktp_pelanggan", reservasi.PelangganID);
            return View(reservasi);
        }

        // GET: reservasis/Edit/5
        /*public ActionResult CheckOut(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservasi reservasi = db.reservasi.Find(id);
            if (reservasi == null)
            {
                return HttpNotFound();
            }
            ViewBag.KamarID = new SelectList(db.kamar, "KamarID", "no_kamar", reservasi.KamarID);
            ViewBag.PelangganID = new SelectList(db.pelanggan, "PelangganID", "nama_pelanggan", reservasi.PelangganID);
            return View(reservasi);
        }

        // POST: reservasis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut([Bind(Include = "ReservasiID,KamarID,PelangganID,tgtl_reserv,tgl_masuk_reserv,tgl_keluar_reserv,lama_reserv,status_reserv")] reservasi reservasi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservasi).State = EntityState.Modified;
                var context = new uasEntities();
                int roompick = reservasi.KamarID;
                var room = (from a in context.kamar
                            where a.KamarID == roompick
                            select a).Single();
                if (reservasi.status_reserv == "in") {
                    room.status_kamar = "kosong";
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.KamarID = new SelectList(db.kamar, "KamarID", "no_kamar", reservasi.KamarID);
            ViewBag.PelangganID = new SelectList(db.pelanggan, "PelangganID", "ktp_pelanggan", reservasi.PelangganID);
            return View(reservasi);
        }*/

        // GET: reservasis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservasi reservasi = db.reservasi.Find(id);
            if (reservasi == null)
            {
                return HttpNotFound();
            }
            return View(reservasi);
        }

        // POST: reservasis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            reservasi reservasi = db.reservasi.Find(id);
            db.reservasi.Remove(reservasi);
            db.SaveChanges();
            var context = new uasEntities();
            int roompick = reservasi.KamarID;
            var room = (from a in context.kamar
                        where a.KamarID == roompick
                        select a).Single();
            room.status_kamar = "kosong";
            context.SaveChanges();
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
