using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstator.Models.Condo
{
    public class CondoCreateModel
    {
        [Key]
        public int CondoID { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Beds { get; set; }
        [Required]
        public int Baths { get; set; }
        [Required]
        public int SquareFootage { get; set; }
        [Required]
        public bool HasPool { get; set; }
        [Required]
        public bool IsWaterfront { get; set; }
        [Required]
        public bool Occupied { get; set; }
        [Required]
        public int YearBuilt { get; set; }
        [Required]
        public int Price { get; set; }
    }
}