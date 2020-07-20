using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstator.Data
{
    public class Request
    {
        public int RequestID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Issue { get; set; }
    }
}