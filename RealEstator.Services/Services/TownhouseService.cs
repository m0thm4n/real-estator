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

        public void CreateTownhouse(TownhouseCreateModel model)
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
            _db.Townhouses.Add(entity);
            _db.SaveChanges();
        }


        public TownhouseDetailsModel TownhouseDetails(int? id)
        {
            var entity = _db.Townhouses.Single(e => e.TownhouseID == id);
            return new TownhouseDetailsModel
            {
                HomeID = entity.TownhouseID,
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

        public Townhouse DeleteTownhouse(int? id)
        {
            var entity = _db.Townhouses.Find(id);
            _db.Townhouses.Remove(entity);
            _db.SaveChanges();

            return entity;
        }

        public Townhouse EditTownhouse(int id, TownhouseEditModel model)
        {
            var townhouseWeWantToEdit = _db.Townhouses.Find(id);
            if (townhouseWeWantToEdit != null)
            {
                townhouseWeWantToEdit.Address = model.Address;
                townhouseWeWantToEdit.Beds = model.Beds;
                townhouseWeWantToEdit.Baths = model.Baths;
                townhouseWeWantToEdit.SquareFootage = model.SquareFootage;
                townhouseWeWantToEdit.HasPool = model.HasPool;
                townhouseWeWantToEdit.IsWaterfront = model.IsWaterfront;
                townhouseWeWantToEdit.Occupied = model.Occupied;
                townhouseWeWantToEdit.YearBuilt = model.YearBuilt;
                townhouseWeWantToEdit.Price = model.Price;

                _db.SaveChanges();

                return townhouseWeWantToEdit;
            }

            return null;
        }
    }
}