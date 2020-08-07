using RealEstator.Contacts;
using RealEstator.Data;
using RealEstator.Data.Entities;
using RealEstator.Models;
using RealEstator.Models.Townhouse;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealEstator.Services
{
    public class TownhouseService : ITownhouseService
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
            _db.Townhouse.Add(entity);
            _db.SaveChanges();
        }

        public IEnumerable<TownhouseListModel> GetTownhouses()
        {
            var entity = _db.Townhouse.Select(
                    e => new TownhouseListModel
                    {
                        TownhouseID = e.TownhouseID,
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

        public TownhouseDetailsModel TownhouseDetails(int? id)
        {
            var entity = _db.Townhouse.Single(e => e.TownhouseID == id);
            return new TownhouseDetailsModel
            {
                TownhouseID = entity.TownhouseID,
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

        public void DeleteTownhouse(int id)
        {
            var entity = _db.Townhouse.Single(e => e.TownhouseID == id);
            _db.Townhouse.Remove(entity);
            _db.SaveChanges();
            _db.SaveChanges();
        }

        public bool EditTownhouse(TownhouseEditModel townhouseToEdit)
        {
            var entity = _db.Home.Single(e => e.HomeID == townhouseToEdit.TownhouseID);
            if (entity != null)
            {
                entity.Address = townhouseToEdit.Address;
                entity.Beds = townhouseToEdit.Beds;
                entity.Baths = townhouseToEdit.Baths;
                entity.SquareFootage = townhouseToEdit.SquareFootage;
                entity.HasPool = townhouseToEdit.HasPool;
                entity.IsWaterfront = townhouseToEdit.IsWaterfront;
                entity.Occupied = townhouseToEdit.Occupied;
                entity.YearBuilt = townhouseToEdit.YearBuilt;
                entity.Price = townhouseToEdit.Price;

                return _db.SaveChanges() == 1;
            }
            return false;
        }

        public TownhouseService()
        {
               
        }
    }
}