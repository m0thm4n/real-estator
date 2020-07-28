using RealEstator.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstator.Data.Entities
{
    [Table("Home")]
    public class Home
    {
        [Key]
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

        [ForeignKey("Request")]
        public int RequestID { get; set; }
        public virtual Request Request { get; set; }
    }
}