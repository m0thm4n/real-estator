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

        public void CreateHome(CondoCreateModel model)
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
            _db.Homes.Add(entity);
            _db.SaveChanges();
        }


        public CondoDetailsModel CondoDetails(int? id)
        {
            var entity = _db.Homes.Single(e => e.HomeId == id);
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
            var entity = _db.Homes.Find(id);
            _db.Homes.Remove(entity);
            _db.SaveChanges();
        }

        public CondoEditModel EditCondo(int id, CondoEditModel model)
        {
            var homeWeWantToEdit = _db.Homes.Find(id);
            if (homeWeWantToEdit != null)
            {
                homeWeWantToEdit.Address = model.Address;
                homeWeWantToEdit.Beds = model.Beds;
                homeWeWantToEdit.Baths = model.Baths;
                homeWeWantToEdit.SquareFootage = model.SquareFootage;
                homeWeWantToEdit.HasPool = model.HasPool;
                homeWeWantToEdit.IsWaterfront = model.IsWaterfront;
                homeWeWantToEdit.Occupied = model.Occupied;
                homeWeWantToEdit.YearBuilt = model.YearBuilt;
                homeWeWantToEdit.Price = model.Price;

                _db.SaveChanges();

                return homeWeWantToEdit;
            }

            return null;
        }
    }
}