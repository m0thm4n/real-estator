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
        void CreateRequest(RequestCreateModel request);
        RequestDetailsModel RequestDetails(int id);
        bool EditRequest(RequestEditModel model);
        IEnumerable<RequestListModel> GetRequests();
        void DeleteRequest(int id);
    }
}
