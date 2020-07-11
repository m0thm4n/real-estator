using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstator.Data;
using RealEstator.Models;
using RealEstator.Models.Condo;

namespace RealEstator.Controllers
{
    public class CondosController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private IHomesService _homeService;

        public HomesController() { }
        public HomesController(IHomesService homeService, ApplicationDbContext db)
        {
            _homeService = homeService;
            _db = db;
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Condoes
        public ActionResult Index()
        {
            return View(_db.Condos.ToList());
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Condoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CondoDetailsModel condo = _db.Condos.Find(id);
            if (condo == null)
            {
                return HttpNotFound();
            }
            return View(condo);
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Condoes/Create
        public ActionResult Create()
        {
            return View(new Condo());
        }
        
        [Authorize(Roles = "Renter,Admin")]
        // POST: Condoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CondoCreateModel condo)
        {
            if (ModelState.IsValid)
            {
                _condoService.CreateCondo(model);
                return RedirectToAction("Index");
            }

            return View(condo);
        }

        [Authorize(Roles = "Renter,Admin")]
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

        [Authorize(Roles = "Renter,Admin")]
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

        [Authorize(Roles = "Renter,Admin")]
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

        [Authorize(Roles = "Renter,Admin")]
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
