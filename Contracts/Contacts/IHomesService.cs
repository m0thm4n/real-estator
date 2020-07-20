using RealEstator.Data;
using RealEstator.Models;
using RealEstator.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstator.Contacts
{
    public interface IHomesService
    {
        void CreateHome(HomeCreateModel home);
        HomeDetailsModel HomeDetails(int? id);
        Home EditHome(int id, HomeEditModel model);
        void DeleteHome(int id);

    }
}
