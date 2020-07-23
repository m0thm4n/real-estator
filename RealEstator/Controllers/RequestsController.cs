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
using RealEstator.Models.Request;
using RealEstator.Services;

namespace RealEstator.Controllers
{
    public class RequestsController : Controller
    {
        private readonly IRequestService _requestService = new RequestService();
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private GoogleMaps _google = new GoogleMaps();

        public RequestsController(IRequestService requestService, ApplicationDbContext db, GoogleMaps google)
        {
            _requestService = requestService;
            _db = db;
            _google = google;
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes
        public ActionResult Index()
        {
            return View(_requestService.GetRequests());
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RequestDetailsModel home = _requestService.RequestDetails(id);
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
        public ActionResult Create(RequestCreateModel request)
        {
            if (ModelState.IsValid)
            {
                _requestService.CreateRequest(request);
                return RedirectToAction("Index");
            }

            return View(request);
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestDetailsModel request = _requestService.RequestDetails(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            var model = new RequestEditModel
            {
                RequestID = request.RequestID,
                Name = request.Name,
                Address = request.Address,
                Issue = request.Issue,
            };
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [Authorize(Roles = "Renter,Admin")]
        // POST: Homes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RequestEditModel requestToEdit)
        {
            if (!ModelState.IsValid) return View(requestToEdit);


            if (requestToEdit.RequestID != id)
            {
                ModelState.AddModelError("", "ID does not match an existing home, please try again.");
                return View(requestToEdit);
            }

            if (_requestService.EditRequest(requestToEdit))
            {
                TempData["SaveResult"] = "You have updated your house.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Could not update home.");
            return View(requestToEdit);
        }

        [Authorize(Roles = "Renter,Admin")]
        // GET: Homes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestDetailsModel home = _requestService.RequestDetails(id);
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
            _requestService.DeleteRequest(id);

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
