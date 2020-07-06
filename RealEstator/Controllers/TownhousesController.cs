using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstator.Models;

namespace RealEstator.Controllers
{
    public class TownhousesController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Townhouses
        public ActionResult Index()
        {
            return View(_db.Townhouses.ToList());
        }

        // GET: Townhouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Townhouse townhouse = _db.Townhouses.Find(id);
            if (townhouse == null)
            {
                return HttpNotFound();
            }
            return View(townhouse);
        }

        // GET: Townhouses/Create
        public ActionResult Create()
        {
            return View(new Townhouse());
        }

        // POST: Townhouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TownhouseID,Address,Beds,Baths,SquareFootage,HasPool,IsWaterfront,Occupied,YearBuilt,Price")] Townhouse townhouse)
        {
            if (ModelState.IsValid)
            {
                _db.Townhouses.Add(townhouse);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(townhouse);
        }

        // GET: Townhouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Townhouse townhouse = _db.Townhouses.Find(id);
            if (townhouse == null)
            {
                return HttpNotFound();
            }
            return View(townhouse);
        }

        // POST: Townhouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TownhouseID,Address,Beds,Baths,SquareFootage,HasPool,IsWaterfront,Occupied,YearBuilt,Price")] Townhouse townhouse)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(townhouse).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(townhouse);
        }

        // GET: Townhouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Townhouse townhouse = _db.Townhouses.Find(id);
            if (townhouse == null)
            {
                return HttpNotFound();
            }
            return View(townhouse);
        }

        // POST: Townhouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Townhouse townhouse = _db.Townhouses.Find(id);
            _db.Townhouses.Remove(townhouse);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
