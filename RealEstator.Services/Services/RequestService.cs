using RealEstator.Contacts;
using RealEstator.Data;
using RealEstator.Models;
using RealEstator.Models.Request;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealEstator.Services
{
    public class RequestService : IRequestService
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

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
            _db.Request.Add(entity);
            _db.SaveChanges();
        }

        public IEnumerable<RequestListModel> GetRequests()
        {
            var entity = _db.Request.Select(
                    e => new RequestListModel
                    {
                        Name = e.Name,
                        Address = e.Address,
                        Issue = e.Issue,
                    }
                );

            return entity.ToList();
        }

        public RequestDetailsModel RequestDetails(int? id)
        {
            var entity = _db.Request.Single(e => e.RequestID == id);
            return new RequestDetailsModel
            {
                Name = entity.Name,
                Address = entity.Address,
                Issue = entity.Issue,
            };
        }

        public void DeleteRequest(int id)
        {
            var entity = _db.Home.Single(e => e.HomeID == id);
            _db.Home.Remove(entity);
            _db.SaveChanges();
            _db.SaveChanges();
        }

        public bool EditRequest(RequestEditModel requestToEdit)
        {
            _db.Entry(requestToEdit).State = EntityState.Modified;

            var entity = _db.Request.Single(e => e.RequestID == requestToEdit.RequestID);
            if (entity != null)
            {
                entity.Name = requestToEdit.Name;
                entity.Address = requestToEdit.Address;
                entity.Issue = requestToEdit.Issue;
                
                return _db.SaveChanges() == 1;
            }
            return false;
        }

        public RequestService()
        {

        }
    }
}