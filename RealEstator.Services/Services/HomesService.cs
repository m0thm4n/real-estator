using RealEstator.Contacts;
using RealEstator.Data;
using RealEstator.Models;
using RealEstator.Models.Home;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace RealEstator.Services
{
    public class HomesService : IHomesService
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public HomesService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateHome(HomeCreateModel model)
        {
            var entity = new Home()
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
            _db.Home.Add(entity);
            _db.SaveChanges();
        }

        public IEnumerable<HomeListModel> GetHomes()
        {
            var entity = _db.Home.Select(
                    e => new HomeListModel
                    {
                        HomeID = e.HomeID,
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

        public HomeDetailsModel HomeDetails(int? id)
        {
            var entity = _db.Home.Single(e => e.HomeID == id);
            return new HomeDetailsModel
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

        public void DeleteHome(int id)
        {
            var entity = _db.Home.Single(e => e.HomeID == id);
            _db.Home.Remove(entity);
            _db.SaveChanges();
            _db.SaveChanges();
        }

        public bool EditHome(HomeEditModel homeToEdit)
        {
            _db.Entry(homeToEdit).State = EntityState.Modified;

            var entity = _db.Home.Single(e => e.HomeID == homeToEdit.HomeID);
            if (entity != null)
            {
                entity.Address = homeToEdit.Address;
                entity.Beds = homeToEdit.Beds;
                entity.Baths = homeToEdit.Baths;
                entity.SquareFootage = homeToEdit.SquareFootage;
                entity.HasPool = homeToEdit.HasPool;
                entity.IsWaterfront = homeToEdit.IsWaterfront;
                entity.Occupied = homeToEdit.Occupied;
                entity.YearBuilt = homeToEdit.YearBuilt;
                entity.Price = homeToEdit.Price;
                
                return _db.SaveChanges() == 1;
            }
            return false;
        }

        public HomesService()
        {

        }
    }
}