using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstator.Contacts;
using RealEstator.Data;
using RealEstator.Models;
using RealEstator.Models.Townhouse;
using RealEstator.Services;

namespace RealEstator.Controllers
{
    public class TownhousesController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private ITownhouseService _townhouseService;

        public TownhousesController() { }
        public TownhousesController(ITownhouseService townhouseService, ApplicationDbContext db)
        {
            _townhouseService = townhouseService;
            _db = db;
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Townhouses
        public ActionResult Index()
        {
            return View(_db.Townhouses.ToList());
        }

        [Authorize(Roles = "Renter,Admin")]
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

        [Authorize(Roles = "Renter,Admin")]
        // GET: Townhouses/Create
        public ActionResult Create()
        {
            return View(new Townhouse());
        }

        [Authorize(Roles = "Renter,Admin")]
        // POST: Townhouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TownhouseCreateModel townhouse)
        {
            if (ModelState.IsValid)
            {
                _townhouseService.CreateTownhouse(townhouse);
                return RedirectToAction("Index");
            }

            return View(townhouse);
        }

        [Authorize(Roles = "Renter,Admin")]
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

        [Authorize(Roles = "Renter,Admin")]
        // POST: Townhouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TownhouseEditModel townhouse)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(townhouse).State = EntityState.Modified;
                _townhouseService.EditTownhouse(id, townhouse);
                return RedirectToAction("Index");
            }
            return View(townhouse);
        }

        [Authorize(Roles = "Renter,Admin")]
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

        [Authorize(Roles = "Renter,Admin")]
        // POST: Townhouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _townhouseService.DeleteTownhouse(id);
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
