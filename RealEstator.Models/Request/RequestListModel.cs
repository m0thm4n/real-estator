using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstator.Models.Request
{
    public class RequestListModel
    {
        public int RequestID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Issue { get; set; }
    }
}
