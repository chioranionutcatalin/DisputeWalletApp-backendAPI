using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testAPI.Models
{
    public class Dispute
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public string Reason { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public bool IGet { get; set; }
        public virtual Client Client { get; set; }
        public int ClientId { get; set; }
        
    }
}