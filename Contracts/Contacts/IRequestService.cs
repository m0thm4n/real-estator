using RealEstator.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstator.Contacts
{
    public interface IRequestService
    {
        void CreateRequest(RequestCreateModel home);
        RequestDetailsModel RequestDetails(int? id);
        RequestEditModel EditHome(int id, RequestEditModel model);
        void DeleteRequest(int id);
    }
}
