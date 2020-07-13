using RealEstator.Data;
using RealEstator.Models;
using RealEstator.Models.Condo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstator.Services
{
    public class CondoService
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
            _db.Condos.Add(entity);
            _db.SaveChanges();
        }


        public CondoDetailsModel CondoDetails(int? id)
        {
            var entity = _db.Homes.Single(e => e.CondoID == id);
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

        public Condo DeleteCondo(int? id)
        {
            var entity = _db.Condos.Find(id);
            _db.Condos.Remove(entity);
            _db.SaveChanges();

            return entity;
        }

        public Condo EditCondo(int id, CondoEditModel model)
        {
            var condoWeWantToEdit = _db.Condos.Find(id);
            if (condoWeWantToEdit != null)
            {
                condoWeWantToEdit.Address = model.Address;
                condoWeWantToEdit.Beds = model.Beds;
                condoWeWantToEdit.Baths = model.Baths;
                condoWeWantToEdit.SquareFootage = model.SquareFootage;
                condoWeWantToEdit.HasPool = model.HasPool;
                condoWeWantToEdit.IsWaterfront = model.IsWaterfront;
                condoWeWantToEdit.Occupied = model.Occupied;
                condoWeWantToEdit.YearBuilt = model.YearBuilt;
                condoWeWantToEdit.Price = model.Price;

                _db.SaveChanges();

                return condoWeWantToEdit;
            }

            return null;
        }
    }
}