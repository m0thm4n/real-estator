using RealEstator.Data;
using RealEstator.Models;
using RealEstator.Models.Townhouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstator.Services
{
    public class TownhouseService
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public TownhouseService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateHome(TownhouseCreateModel model)
        {
            var entity = new Townhouse()
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


        public TownhouseDetailsModel TownhouseDetails(int? id)
        {
            var entity = _db.Homes.Single(e => e.HomeId == id);
            return new TownhouseDetailsModel
            {
                HomeID = entity.HomeID,
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

        public TownhouseDeleteModel DeleteTownhouse(int? id)
        {
            var entity = _db.Homes.Find(id);
            _db.Homes.Remove(entity);
            _db.SaveChanges();

            return entity;
        }

        public TownhouseEditModel EditTownhouse(int id, TownhouseEditModel model)
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