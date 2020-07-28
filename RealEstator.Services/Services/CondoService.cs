using RealEstator.Contacts;
using RealEstator.Data;
using RealEstator.Data.Entities;
using RealEstator.Models;
using RealEstator.Models.Condo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealEstator.Services
{
    public class CondoService : ICondoService
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public CondoService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateCondo(CondoCreateModel model)
        {
            var entity = new Condo()
            {
                Address = model.Address,
                Beds = model.Beds,
                Baths = model.Baths,
                SquareFootage = model.SquareFootage,
                HasPool = model.HasPool,
                IsWaterfront = model.IsWaterfront,
                Occupied = model.Occupied,
                YearBuilt = model.YearBuilt,
                Price = model.Price,
            };
            _db.Condo.Add(entity);
            _db.SaveChanges();
        }

        public IEnumerable<CondoListModel> GetCondos()
        {
            var entity = _db.Condo.Select(
                    e => new CondoListModel
                    {
                        CondoID = e.CondoID,
                        Address = e.Address,
                        Beds = e.Beds,
                        Baths = e.Baths,
                        SquareFootage = e.SquareFootage,
                        HasPool = e.HasPool,
                        IsWaterfront = e.IsWaterfront,
                        Occupied = e.Occupied,
                        YearBuilt = e.YearBuilt,
                        Price = e.Price,
                    }
                );

            return entity.ToList();
        }

        public CondoDetailsModel CondoDetails(int? id)
        {
            var entity = _db.Condo
                .Single(e => e.CondoID == id);
            return new CondoDetailsModel
            {
                CondoID = entity.CondoID,
                Address = entity.Address,
                Beds = entity.Beds,
                Baths = entity.Baths,
                SquareFootage = entity.SquareFootage,
                HasPool = entity.HasPool,
                IsWaterfront = entity.IsWaterfront,
                Occupied = entity.Occupied,
                YearBuilt = entity.YearBuilt,
                Price = entity.Price,
            };
        }

        public void DeleteCondo(int id)
        {
            var entity = _db.Condo.Single(e => e.CondoID == id);
            _db.Condo.Remove(entity);
            _db.SaveChanges();
        }

        public bool EditCondo(CondoEditModel condoToEdit)
        {
            _db.Entry(condoToEdit).State = EntityState.Modified;

            var entity = _db.Home.Single(e => e.HomeID == condoToEdit.CondoID);
            if (entity != null)
            {
                entity.Address = condoToEdit.Address;
                entity.Beds = condoToEdit.Beds;
                entity.Baths = condoToEdit.Baths;
                entity.SquareFootage = condoToEdit.SquareFootage;
                entity.HasPool = condoToEdit.HasPool;
                entity.IsWaterfront = condoToEdit.IsWaterfront;
                entity.Occupied = condoToEdit.Occupied;
                entity.YearBuilt = condoToEdit.YearBuilt;
                entity.Price = condoToEdit.Price;

                return _db.SaveChanges() == 1;
            }
            return false;
        }

        public CondoService()
        {

        }
    }
}