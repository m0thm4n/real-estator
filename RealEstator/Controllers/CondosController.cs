using System;
using System.Collections.Generic;
using System.Data;
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
using RealEstator.Models.Condo;
using RealEstator.Models.Home;
using RealEstator.Services;

namespace RealEstator.Controllers
{
    public class CondosController : Controller
    {
        private readonly ICondoService _condoService = new CondoService();
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private GoogleMaps _google = new GoogleMaps();

        public CondosController(ICondoService condoService, ApplicationDbContext db, GoogleMaps google)
        {
            _condoService = condoService;
            _db = db;
            _google = google;
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Condoes
        public ActionResult Index()
        {
            return View(_condoService.GetCondos());
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Condoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CondoDetailsModel home = _condoService.CondoDetails(id);
            if (home == null)
            {
                return HttpNotFound();
            }

            var apiKey = GetConfig.LoadConfig();

            var map = _google.GetMaps(apiKey, home.Address);
            var url = map.ToUri().AbsoluteUri;

            url += "&key=" + apiKey;
            ViewBag.Static = url.Replace("%20", "+").Replace("%2C", ",");
            return View(home);
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Condoes/Create
        public ActionResult Create()
        {
            return View(new CondoCreateModel());
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
                _condoService.CreateCondo(condo);
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
            CondoDetailsModel condo = _condoService.CondoDetails(id);
            if (condo == null)
            {
                return HttpNotFound();
            }
            var model = new CondoEditModel
            {
                CondoID = condo.CondoID,
                Address = condo.Address,
                Beds = condo.Beds,
                Baths = condo.Baths,
                SquareFootage = condo.SquareFootage,
                HasPool = condo.HasPool,
                IsWaterfront = condo.IsWaterfront,
                Occupied = condo.Occupied,
                YearBuilt = condo.YearBuilt,
                Price = condo.Price,
            };
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [Authorize(Roles = "Renter,Admin")]
        // POST: Condoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CondoEditModel condoToEdit)
        {
            if (!ModelState.IsValid) return View(condoToEdit);


            if (condoToEdit.CondoID != id)
            {
                ModelState.AddModelError("", "ID does not match an existing home, please try again.");
                return View(condoToEdit);
            }

            if (_condoService.EditCondo(condoToEdit))
            {
                TempData["SaveResult"] = "You have updated your house.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Could not update home.");
            return View(condoToEdit);
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Condoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CondoDetailsModel condo = _condoService.CondoDetails(id);
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
            _condoService.DeleteCondo(id);
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
