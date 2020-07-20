using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstator.Models.Request
{
    public class RequestDeleteModel
    {
        public int RequestID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Issue { get; set; }
    }
}