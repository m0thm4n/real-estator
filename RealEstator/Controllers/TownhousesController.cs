using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoogleAPI;
using RealEstator.Config;
using RealEstator.Contacts;
using RealEstator.Data;
using RealEstator.Models;
using RealEstator.Models.Townhouse;
using RealEstator.Services;

namespace RealEstator.Controllers
{
    public class TownhousesController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly ITownhouseService _townhouseService = new TownhouseService();
        private GoogleMaps _google = new GoogleMaps();

        public TownhousesController() { }
        public TownhousesController(ITownhouseService townhouseService, ApplicationDbContext db, GoogleMaps google)
        {
            _townhouseService = townhouseService;
            _db = db;
            _google = google;
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes
        public ActionResult Index()
        {
            return View(_townhouseService.GetTownhouses());
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TownhouseDetailsModel home = _townhouseService.TownhouseDetails(id);
            if (home == null)
            {
                return HttpNotFound();
            }

            var apiKey = GetConfig.LoadConfig();

            var map = _google.GetMaps(apiKey, home.Address);
            ViewBag.Static = map.ToUri();
            return View(home);
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes/Create
        public ActionResult Create()
        {
            return View(new Home());
        }

        [Authorize(Roles = "Renter,Admin")]
        // POST: Homes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TownhouseCreateModel home)
        {
            if (ModelState.IsValid)
            {
                _townhouseService.CreateTownhouse(home);
                return RedirectToAction("Index");
            }

            return View(home);
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes/Edit/5
        public ActionResult Edit(int id)
        {
            var home = _townhouseService.TownhouseDetails(id);
            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }

        [Authorize(Roles = "Renter,Admin")]
        // POST: Homes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TownhouseEditModel townhouseToEdit)
        {
            if (!ModelState.IsValid) return View(townhouseToEdit);


            if (townhouseToEdit.TownhouseID != id)
            {
                ModelState.AddModelError("", "ID does not match an existing home, please try again.");
                return View(townhouseToEdit);
            }

            if (_townhouseService.EditTownhouse(townhouseToEdit))
            {
                TempData["SaveResult"] = "You have updated your house.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Could not update home.");
            return View(townhouseToEdit);
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TownhouseDetailsModel home = _townhouseService.TownhouseDetails(id);
            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }

        [Authorize(Roles = "Renter,Admin")]
        // POST: Homes/Delete/5
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
