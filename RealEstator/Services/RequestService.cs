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
            _db.Homes.Add(entity);
            _db.SaveChanges();
        }

        public RequestDetailsModel RequestDetails(int? id)
        {
            var entity = _db.Homes.Single(e => e.HomeID == id);
            return new RequestDetailsModel
            {
                RequestID = model.RequestId,
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

        public RequestEditModel EditRequest(int id, RequestEditModel model)
        {
            var homeWeWantToEdit = _db.Homes.Find(id);
            if (homeWeWantToEdit != null)
            {
                Name = model.Name;
                Address = model.Address;
                Issue = model.Issue;

                _db.SaveChanges();

                return homeWeWantToEdit;
            }

            return null;
        }
    }
}