using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RealEstator.Contacts;
using RealEstator.Data;
using RealEstator.Models.Home;
using GoogleAPI;
using RealEstator.Config;

namespace RealEstator.Controllers
{
    public class HomesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IHomesService homeService;
        private GoogleMaps google;

        public HomesController() { }
        public HomesController(IHomesService homeService, ApplicationDbContext db, GoogleMaps google)
        {
            this.homeService = homeService;
            this.db = db;
            this.google = google;
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes
        public ActionResult Index()
        {
            return View(db.Homes.ToList());
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Home home = db.Homes.Find(id);
                if (home == null)
                {
                    return HttpNotFound();
                }

                var apiKey = GetConfig.LoadConfig();

                var map = google.GetMaps(apiKey, home.Address);
                ViewBag["StaticMapUri"] = map.ToUri();
                return View(home);
            }
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
        public ActionResult Create(HomeCreateModel home)
        {
            if (ModelState.IsValid)
            {
                homeService.CreateHome(home);
                return RedirectToAction("Index");
            }

            return View(home);
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes/Edit/5
        public ActionResult Edit(int id)
        {
            var home = homeService.HomeDetails(id);
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
        public ActionResult Edit(int id, HomeEditModel homeToEdit)
        {
            if (!ModelState.IsValid) return View(homeToEdit);


            if (homeToEdit.HomeID != id)
            {
                ModelState.AddModelError("", "ID does not match an existing home, please try again.");
                return View(homeToEdit);
            }

            if (homeService.EditHome(homeToEdit))
            {
                TempData["SaveResult"] = "You have updated your house.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Could not update home.");
            return View(homeToEdit);
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Home home = db.Homes.Find(id);
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
