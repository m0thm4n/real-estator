using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstator.Models
{
    public class Condo
    {
        [Key]
        public int CondoID { get; set; }
        public string Address { get; set; }
        public int Rooms { get; set; }
        public bool HasPool { get; set; }
    }
}