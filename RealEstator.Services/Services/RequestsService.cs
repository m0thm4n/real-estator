using RealEstator.Contacts;
using RealEstator.Data;
using RealEstator.Data.Entities;
using RealEstator.Models;
using RealEstator.Models.Request;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;

namespace RealEstator.Services
{
    public class RequestsService : IRequestService
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public RequestsService(ApplicationDbContext db)
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
                        RequestID = e.RequestID,
                        Name = e.Name,
                        Address = e.Address,
                        Issue = e.Issue,
                    }
                );

            return entity.ToList();
        }

        public RequestDetailsModel RequestDetails(int id)
        {
            var entity = _db.Request.SingleOrDefault(e => e.RequestID == id);

            return new RequestDetailsModel
            {
                RequestID = entity.RequestID,
                Name = entity.Name,
                Address = entity.Address,
                Issue = entity.Issue,
            };
        }

        public void DeleteRequest(int id)
        {
            var entity = _db.Request.Single(e => e.RequestID == id);
            _db.Request.Remove(entity);
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

        public RequestsService()
        {

        }
    }
}