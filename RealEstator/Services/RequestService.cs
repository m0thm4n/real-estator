using RealEstator.Data;
using RealEstator.Models;
using RealEstator.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstator.Services
{
    public class RequestService
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public RequestService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateRequest(RequestCreateModel model)
        {
            var entity = new Request()
            {
                Name = model.Name,
                Address = model.Address,
                Issue = model.Issue,
            };
            _db.Requests.Add(entity);
            _db.SaveChanges();
        }

        public RequestDetailsModel RequestDetails(int? id, RequestDetailsModel model)
        {
            var entity = _db.Requests.Single(e => e.RequestID == id);
            return new RequestDetailsModel
            {
                RequestID = model.RequestID,
                Name = model.Name,
                Address = model.Address,
                Issue = model.Issue,
            };
        }

        public void DeleteRequest(int id)
        {
            var entity = _db.Homes.Find(id);
            _db.Homes.Remove(entity);
            _db.SaveChanges();
        }

        public Request EditRequest(int id, RequestEditModel model)
        {
            var homeWeWantToEdit = _db.Requests.Find(id);
            if (homeWeWantToEdit != null)
            {
                homeWeWantToEdit.Name = model.Name;
                homeWeWantToEdit.Address = model.Address;
                homeWeWantToEdit.Issue = model.Issue;

                _db.SaveChanges();

                return homeWeWantToEdit;
            }

            return null;
        }
    }
}