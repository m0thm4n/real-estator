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
        bool EditTownhouse(TownhouseEditModel model);
        IEnumerable<TownhouseListModel> GetTownhouses();
        void DeleteTownhouse(int id);
    }
}
