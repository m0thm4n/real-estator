using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RealEstator.Contacts;
using RealEstator.Data;
using RealEstator.Models.Home;
using GoogleAPI;
using RealEstator.Config;
using RealEstator.Services;

namespace RealEstator.Controllers
{
    public class HomesController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly IHomesService _homeService = new HomesService();
        private GoogleMaps _google = new GoogleMaps();

        public HomesController() { }
        public HomesController(IHomesService homeService, ApplicationDbContext db, GoogleMaps google)
        {
            _homeService = homeService;
            _db = db;
            _google = google;
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes
        public ActionResult Index()
        {
            return View(_homeService.GetHomes());
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HomeDetailsModel home = _homeService.HomeDetails(id);
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
        public ActionResult Create(HomeCreateModel home)
        {
            if (ModelState.IsValid)
            {
                _homeService.CreateHome(home);
                return RedirectToAction("Index");
            }

            return View(home);
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes/Edit/5
        public ActionResult Edit(int id)
        {
            HomeDetailsModel home = _homeService.HomeDetails(id);
            if (home == null)
            {
                return HttpNotFound();
            }
            var model = new HomeEditModel
            {
                HomeID = home.HomeID,
                Address = home.Address,
                Beds = home.Beds,
                Baths = home.Baths,
                SquareFootage = home.SquareFootage,
                HasPool = home.HasPool,
                IsWaterfront = home.IsWaterfront,
                Occupied = home.Occupied,
                YearBuilt = home.YearBuilt,
                Price = home.Price,
            };
            return View(model);
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

            if (_homeService.EditHome(homeToEdit))
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
            HomeDetailsModel home = _homeService.HomeDetails(id);
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
            _homeService.DeleteHome(id);

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
