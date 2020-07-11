using RealEstator.Models.Townhouse;
using RealEstator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstator.Contacts
{
    public interface ITownhouseService
    {
        void CreateTownhouse(TownhouseCreateModel home);
        TownhouseDetailsModel TownhouseDetails(int? id);
        TownhouseEditModel EditHome(int id, TownhouseEditModel model);
        void DeleteTownhouse(int id);
    }
}
