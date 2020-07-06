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
    public class CondosController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Condoes
        public ActionResult Index()
        {
            return View(_db.Condos.ToList());
        }

        // GET: Condoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condo condo = _db.Condos.Find(id);
            if (condo == null)
            {
                return HttpNotFound();
            }
            return View(condo);
        }

        // GET: Condoes/Create
        public ActionResult Create()
        {
            return View(new Condo());
        }

        // POST: Condoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CondoID,Address,Rooms,SquareFootage,HasPool,IsWaterfront,Occupied,YearBuilt,Price")] Condo condo)
        {
            if (ModelState.IsValid)
            {
                _db.Condos.Add(condo);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(condo);
        }

        // GET: Condoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condo condo = _db.Condos.Find(id);
            if (condo == null)
            {
                return HttpNotFound();
            }
            return View(condo);
        }

        // POST: Condoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CondoID,Address,Rooms,SquareFootage,HasPool,IsWaterfront,Occupied,YearBuilt,Price")] Condo condo)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(condo).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(condo);
        }

        // GET: Condoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condo condo = _db.Condos.Find(id);
            if (condo == null)
            {
                return HttpNotFound();
            }
            return View(condo);
        }

        // POST: Condoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Condo condo = _db.Condos.Find(id);
            _db.Condos.Remove(condo);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
