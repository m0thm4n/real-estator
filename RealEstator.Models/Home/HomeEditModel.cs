using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstator.Models.Home
{
    public class HomeEditModel
    {
        public int HomeID { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
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