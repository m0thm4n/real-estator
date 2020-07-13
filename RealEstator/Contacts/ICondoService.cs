using RealEstator.Models.Condo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstator.Contacts
{
    public interface ICondoService
    {
        void CreateCondo(CondoCreateModel condo);
        CondoDetailsModel CondoDetails(int? id);
        CondoEditModel EditCondo(int id, CondoEditModel model);
        CondoDeleteModel DeleteCondo(int? id);
    }
}
