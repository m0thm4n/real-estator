using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstator.Models
{
    public class Home
    {
        [Key]
        public int HomeID { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Rooms { get; set; }
        [Required]
        public bool HasPool { get; set; }
    }
}