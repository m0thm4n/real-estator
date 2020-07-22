using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstator.Models.Home
{
    public class HomeListModel
    {
        public int HomeID { get; set; }
        public string Address { get; set; }
        public int Beds { get; set; }
        public int Baths { get; set; }
        public int SquareFootage { get; set; }
        public bool HasPool { get; set; }
        public bool IsWaterfront { get; set; }
        public bool Occupied { get; set; }
        public int YearBuilt { get; set; }
        public float Price { get; set; }
    }
}
