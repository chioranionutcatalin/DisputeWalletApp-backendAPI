using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testAPI.Models;

namespace testAPI.viewmodels
{
    public class DisputeModel
    {
       // public string Token { get; set; }

        public int ClientId { get; set; }
        public float Amount { get; set; }
        public string Reason { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public bool IGet { get; set; }
        public string Token { get; set; }
        //public virtual Client Client { get; set; }
    }
}