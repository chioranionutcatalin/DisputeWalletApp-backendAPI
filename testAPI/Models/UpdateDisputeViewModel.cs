using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testAPI.Models
{
    public class UpdateDisputeViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public float Amount { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public float Latitude { get; set; }
        [Required]
        public float Longitude { get; set; }
        public bool IGet { get; set; }
    }
}